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
    public partial class orders : Form
    {
        private readonly HttpClient _httpClient;
        public orders()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7193/api/Orders");
            InitializeComponent();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            //בקשת GET מהשרת
            HttpResponseMessage respons = await _httpClient.GetAsync(_httpClient.BaseAddress);

            if (respons.IsSuccessStatusCode)
            {

                //JSONקבלת הנתונים והמרתם ל
                String dataStr = await respons.Content.ReadAsStringAsync();
                MessageBox.Show(dataStr);

                List<OrdersDTO> list = JsonConvert.DeserializeObject<List<OrdersDTO>>(dataStr);

                //הצגת הנתונים בטופס
                dataGridView1.DataSource = list;
            }

            else
                MessageBox.Show("error: " + respons.StatusCode);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            OrdersDTO dto = new OrdersDTO();
            dto.OrdNum = int.Parse(row.Cells[0].Value.ToString());
            dto.CustId = int.Parse(row.Cells[1].Value.ToString());
            dto.CustName = row.Cells[2].Value == null ? "" : row.Cells[2].Value.ToString();
            dto.ProdId = int.Parse(row.Cells[3].Value.ToString());
            dto.ProdName = row.Cells[4].Value ==null? "" :row.Cells[4].Value.ToString();
            dto.Qty = int.Parse(row.Cells[5].Value.ToString());
            dto.OrdDate = DateTime.Parse(row.Cells[6].Value.ToString());

            string data = JsonConvert.SerializeObject(dto);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await _httpClient.PutAsync(_httpClient.BaseAddress , content);

            if (respons.IsSuccessStatusCode && int.Parse(respons.Content.ReadAsStringAsync().Result) > 0)
                MessageBox.Show("success!!!");
            else
                MessageBox.Show("not success,sory " );

        }


        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            button5.Visible = true;
        }


        private async void button5_Click(object sender, EventArgs e)
        {
            OrdersDTO ordersDTO = new OrdersDTO();
            ordersDTO.CustId = int.Parse(textBox1.Text);
            ordersDTO.ProdId = int.Parse(textBox2.Text);
            ordersDTO.Qty = int.Parse(textBox3.Text);
            ordersDTO.OrdDate = DateTime.Parse(textBox4.Text);
            ordersDTO.CustName = "";
            ordersDTO.ProdName = "";
            

            string data = JsonConvert.SerializeObject(ordersDTO);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage respons = await _httpClient.PostAsync(_httpClient.BaseAddress, content);
            if (respons.IsSuccessStatusCode)
                MessageBox.Show("added successfuly");
            else
                MessageBox.Show("not success");
        }

        private async void button1_Click(object sender, EventArgs e)
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
