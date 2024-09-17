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
    public partial class product : Form
    {

        private readonly HttpClient _httpClient;
        public product()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7193/api/Products");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            button5.Visible = true;
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

                List<ProductsDTO> list = JsonConvert.DeserializeObject<List<ProductsDTO>>(dataStr);
                dataGridView1.DataSource = list;
            }

            else
                MessageBox.Show("error: " + respons.StatusCode);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            ProductsDTO productsDTO = new ProductsDTO();
            productsDTO.ProdDesc = row.Cells[1].Value.ToString();
            productsDTO.Cost=double.Parse(row.Cells[2].Value.ToString());

            string data = JsonConvert.SerializeObject(productsDTO);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await _httpClient.PutAsync(_httpClient.BaseAddress, content);

            if (respons.IsSuccessStatusCode && int.Parse(respons.Content.ReadAsStringAsync().Result) > 0)
                MessageBox.Show("success!!!");
            else
                MessageBox.Show("not success,sory ");
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            ProductsDTO pro=new ProductsDTO();
            pro.ProdDesc = textBox1.Text;
            pro.Cost=double.Parse(textBox2.Text);

            string data = JsonConvert.SerializeObject(pro);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage respons = await _httpClient.PostAsync(_httpClient.BaseAddress, content);
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
