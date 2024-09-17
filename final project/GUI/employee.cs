using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.models;
using Newtonsoft.Json;

namespace GUI
{
    public partial class employee : Form
    {
        private readonly HttpClient _httpClient;
        public employee()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7193/api/Employee");
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //בקשת GET מהשרת
            HttpResponseMessage respons = await _httpClient.GetAsync(_httpClient.BaseAddress);

            if (respons.IsSuccessStatusCode)
            {
               
                //JSONקבלת הנתונים והמרתם ל
                String dataStr = await respons.Content.ReadAsStringAsync();
                MessageBox.Show(dataStr);

                List<EmployeeDTO> list = JsonConvert.DeserializeObject<List<EmployeeDTO>>(dataStr);
               
                //הצגת הנתונים בטופס
                dataGridView1.DataSource = list;
            }

            else
                MessageBox.Show("error: " + respons.StatusCode);
        }

        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            EmployeeDTO emp = new EmployeeDTO();
            DataGridViewRow row = dataGridView1.SelectedRows[0];

            emp.EmpId = row.Cells[0].Value?.ToString();
            emp.LastName=row.Cells[1].Value?.ToString();
            emp.FirstName=row.Cells[2].Value?.ToString();
            emp.Zip=row.Cells[3].Value?.ToString();
            emp.Phone=row.Cells[4].Value?.ToString();
            emp.Address=row.Cells[5].Value?.ToString();
            emp.City=row.Cells[6].Value?.ToString();


            string data = JsonConvert.SerializeObject(emp);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await _httpClient.PutAsync(_httpClient.BaseAddress , content);

            if (respons.IsSuccessStatusCode && int.Parse(respons.Content.ReadAsStringAsync().Result) > 0)
                MessageBox.Show("success!!!");
            else
                MessageBox.Show("not success,sory ");

        }

        private async void button3_ClickAsync(object sender, EventArgs e)
        {
          
             
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            textBox7.Visible = true;
            

            EmployeeDTO empDTO = new EmployeeDTO();
            empDTO.EmpId=textBox1.Text;
            empDTO.LastName=textBox2.Text;
            empDTO.FirstName=textBox3.Text;
            empDTO.Zip=textBox4.Text;
            empDTO.Phone=textBox5.Text;
            empDTO.Address=textBox6.Text;
            empDTO.City=textBox7.Text;

            string data = JsonConvert.SerializeObject(empDTO);
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
