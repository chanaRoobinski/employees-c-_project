using GUI.models;
using Newtonsoft.Json;
using System;
//using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class customers : Form
    {
        private readonly HttpClient _httpClient;
        public customers()
        {
            //יצירת אובייקט לתקשורת עם השרת
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7193/api/Customers");
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //בקשת GET מהשרת
            HttpResponseMessage respons = await _httpClient.GetAsync(_httpClient.BaseAddress);

            if (respons.IsSuccessStatusCode)
            {
                //הצגת הנתונים בטופס
                //JSONקבלת הנתונים והמרתם ל
                String dataStr = await respons.Content.ReadAsStringAsync();
                MessageBox.Show(dataStr);

                List<CustomerDTO> list = JsonConvert.DeserializeObject<List<CustomerDTO>>(dataStr);
                dataGridView1.DataSource = list;
            }

            else
                MessageBox.Show("error: " + respons.StatusCode);
        }


        private async void button2_Click_1(object sender, EventArgs e)
        {

            CustomerDTO cust = new CustomerDTO();
            DataGridViewRow row = dataGridView1.SelectedRows[0];

            cust.CustId = int.Parse(row.Cells[0].Value.ToString());
            cust.CustName = row.Cells[1].Value.ToString();
            cust.CustAddress = row.Cells[2].Value.ToString();
            cust.CustCity = row.Cells[3].Value.ToString();
            cust.CustPhone = row.Cells[4].Value.ToString();
            cust.CustFax = row.Cells[5].Value?.ToString();
            cust.EmpId = row.Cells[6].Value.ToString();
            cust.EmpFName = row.Cells[7].Value.ToString();
            cust.EmpLName = row.Cells[8].Value.ToString();

            string data = JsonConvert.SerializeObject(cust);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respons =await  _httpClient.PutAsync(_httpClient.BaseAddress , content);

            if (respons.IsSuccessStatusCode && int.Parse(respons.Content.ReadAsStringAsync().Result) > 0)
                MessageBox.Show("success!!!");
            else
                MessageBox.Show("not success,sory ");
        }

        private async void button3_Click(object sender, EventArgs e)
        {

            textBox1.Visible = true;label1.Visible = true;
            textBox2.Visible = true; label2.Visible = true;
            textBox3.Visible = true; label3.Visible = true;
            textBox4.Visible = true; label4.Visible = true;
            textBox5.Visible = true; label5.Visible = true;
            textBox6.Visible = true; label6.Visible = true;
            textBox7.Visible = true; label7.Visible = true;
            textBox8.Visible = true; label8.Visible = true;


            CustomerDTO customer = new CustomerDTO();
            customer.CustName = textBox1.Text;
            customer.CustAddress = textBox2.Text;
            customer.CustCity= textBox3.Text;
            customer.CustPhone = textBox4.Text;
            customer.CustFax= textBox5.Text;
            customer.EmpId = textBox6.Text;
            customer.EmpFName = textBox7.Text;
            customer.EmpLName = textBox8.Text;

            string data = JsonConvert.SerializeObject(customer);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage respons = await _httpClient.PostAsync(_httpClient.BaseAddress , content);
            if (respons.IsSuccessStatusCode)
                MessageBox.Show("added successfuly");
            else
                MessageBox.Show("not success");

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var i = dataGridView1.SelectedRows[0].Cells[0].Value;
            HttpResponseMessage respons = await _httpClient.DeleteAsync(_httpClient.BaseAddress + "/" + i);

            if (respons.IsSuccessStatusCode)
                MessageBox.Show("delete successfuly");
            else
                MessageBox.Show("not success to delete");
        }
     }
}