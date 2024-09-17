using GUI.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class EmployeePay : Form
    {
        private readonly HttpClient _httpClient;
        public EmployeePay()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7193/api/EmployeePay");
            InitializeComponent();
        }


        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            //בקשת GET מהשרת
            HttpResponseMessage respons = await _httpClient.GetAsync(_httpClient.BaseAddress);

            if (respons.IsSuccessStatusCode)
            {

                //JSONקבלת הנתונים והמרתם ל
                String dataStr = await respons.Content.ReadAsStringAsync();
                MessageBox.Show(dataStr);

                List<EmployeePayDTO> list = JsonConvert.DeserializeObject<List<EmployeePayDTO>>(dataStr);

                //הצגת הנתונים בטופס
                dataGridView1.DataSource = list;
            }

            else
                MessageBox.Show("error: " + respons.StatusCode);
        }



        private async void button2_Click(object sender, EventArgs e)
         {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            EmployeePayDTO empPay = new EmployeePayDTO();
             int  x=0;

            empPay.EmpId = row.Cells[0].Value.ToString();
            if(int.TryParse(row.Cells[1].Value?.ToString(),out x))
            empPay.Position = x;
            empPay.DateHire = DateTime.Parse(row.Cells[2].Value.ToString());
            if(int.TryParse(row.Cells[3].Value?.ToString(), out x))
            empPay.Bonus = x;
            empPay.DateLastRaise = DateTime.Parse(row.Cells[4].Value.ToString());
            if(int.TryParse(row.Cells[5].Value?.ToString(), out x))
            empPay.Bonus = x;
            if(int.TryParse(row.Cells[6].Value?.ToString() ,out x) )
            empPay.Bonus = x;
            empPay.EmpLName = row.Cells[7].Value.ToString();
            empPay.EmpFName = row.Cells[8].Value.ToString();

            string data = JsonConvert.SerializeObject(empPay);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await _httpClient.PutAsync(_httpClient.BaseAddress, content);

            if (respons.IsSuccessStatusCode && int.Parse(respons.Content.ReadAsStringAsync().Result) > 0)
                MessageBox.Show("success!!!");
            else
                MessageBox.Show("not success,sory ");
        }


        private async void button3_Click(object sender, EventArgs e)
         {
            
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = true;
            button5.Visible = true;
           
             
            
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

        private async void button5_Click(object sender, EventArgs e)
        {
            EmployeePayDTO empPay = new EmployeePayDTO();

            empPay.EmpId = textBox1.Text;
            empPay.Position = int.Parse(textBox2.Text);
            empPay.DateHire = DateTime.Parse(textBox3.Text);
            empPay.Payrate = int.Parse(textBox4.Text);
            empPay.DateLastRaise = DateTime.Parse(textBox5.Text);
            empPay.Salary = int.Parse(textBox6.Text);
            empPay.Bonus = int.Parse(textBox7.Text);
            empPay.EmpLName = textBox8.Text;
            empPay.EmpFName = textBox9.Text;


            string data = JsonConvert.SerializeObject(empPay);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage respons = await _httpClient.PostAsync(_httpClient.BaseAddress, content);
            if (respons.IsSuccessStatusCode)
                MessageBox.Show("added successfuly");
            else
                MessageBox.Show("not success");
        }
    }
}
