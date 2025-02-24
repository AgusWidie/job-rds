using Desktop_Warranty_TSJ.Help;
using Desktop_Warranty_TSJ.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Desktop_Warranty_TSJ
{
    public partial class TemplateDataPrint : Form
    {
        public TemplateDataPrint()
        {
            InitializeComponent();
        }

        private async void Cari_Click(object sender, EventArgs e)
        {
            List<TemplatePrint> ListTemplatePrint = new List<TemplatePrint>();

            string TemplateNameVar = TemplateName.Text;

            ListTemplatePrint = await GetDataTemplatePrint(TemplateNameVar, CommonVariable.SourcePrinter);

            if(ListTemplatePrint.Count() <= 0)
            {
                lstViewTemplatePrint.Items.Clear();
                MessageBox.Show("Data Template Print Tidak Ada.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            lstViewTemplatePrint.Items.Clear();

            int no = 1;
            foreach (var item in ListTemplatePrint)
            {
                
                ListViewItem lvi = new ListViewItem();
                lvi.Text = no.ToString();
                lvi.SubItems.Add(item.TemplateName);
                lvi.SubItems.Add(item.Source);
                if(item.Active == true)
                {
                    lvi.SubItems.Add("TRUE");

                } else {

                    lvi.SubItems.Add("FALSE");
                }

                if (item.PrintDefault == true) {
                    lvi.SubItems.Add("TRUE");

                } else {

                    lvi.SubItems.Add("FALSE");
                }
                lvi.SubItems.Add(item.Base64Logo);

                lstViewTemplatePrint.Items.Add(lvi);

                if (no == 500)
                {
                    Application.DoEvents();
                }
                no++;
            }
        }

        public static async Task<List<TemplatePrint>> GetDataTemplatePrint(string TemplateName, string Source)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            HttpClient client = new HttpClient();
            List<TemplatePrint> resData = new List<TemplatePrint>();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CommonVariable.Token);
            var response = await client.GetAsync(CommonVariable.baseUrl + "/TemplatePrint/ListGetDataTemplatePrint?TemplateName=" + TemplateName + "&Source=" + CommonVariable.SourcePrinter);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseListSuccessAPI(responseContent, Convert.ToInt32(response.StatusCode));
            }
            else
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseListErrorAPI(responseContent, Convert.ToInt32(response.StatusCode));
            }

            if (res.Error == false)
            {
                var dataList = JsonSerializer.Deserialize<List<TemplatePrint>>(JsonSerializer.Serialize(res.Data),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

                resData = dataList;
                return resData;

            }
            else
            {

                MessageBox.Show("Error GetDataTemplatePrint : " + res.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return resData;
            }
        }

        private void TemplatePrint_Load(object sender, EventArgs e)
        {
            // Inisialisasi ListView
            lstViewTemplatePrint.View = View.Details;  // Mengatur tampilan menjadi mode detail agar ada header kolom
            lstViewTemplatePrint.FullRowSelect = true;  // Memilih seluruh baris saat diklik
            lstViewTemplatePrint.GridLines = true;      // Menampilkan garis pembatas antar baris
            lstViewTemplatePrint.Scrollable = true; // Enable scrolling
            lstViewTemplatePrint.MultiSelect = true;

            // Menambahkan kolom (header) ke ListView
            lstViewTemplatePrint.Columns.Add("No.", 50);
            lstViewTemplatePrint.Columns.Add("Nama Template", 150);
            lstViewTemplatePrint.Columns.Add("Sumber", 100);
            lstViewTemplatePrint.Columns.Add("Aktif", 100);
            lstViewTemplatePrint.Columns.Add("Print Default", 120);
            lstViewTemplatePrint.Columns.Add("File Base64", 250);

            SumberPrint.Text = CommonVariable.SourcePrinter;
            this.AutoSize = false;
        }

        private async void Hapus_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            GlobalObjectResponse res = new GlobalObjectResponse();
            string TemplateNameVar = "";

            if (TemplateName.Text == "")
            {

                MessageBox.Show("Nama Template Masih Kosong. Mohon Dicari Dahulu Datanya.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TemplateName.Focus();
                return;
            }
            TemplateNameVar = TemplateName.Text;

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CommonVariable.Token);
            var response = await client.DeleteAsync(CommonVariable.baseUrl + "/TemplatePrint/DeleteTemplatePrint?TemplateName=" + TemplateNameVar.Trim() + "&Source=" + CommonVariable.SourcePrinter);
            if (response.IsSuccessStatusCode) {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(response.StatusCode));

            } else {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(response.StatusCode));
                MessageBox.Show("Error DeleteTemplatePrint : " + res.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<TemplatePrint> ListTemplatePrint = new List<TemplatePrint>();

            ListTemplatePrint = await GetDataTemplatePrint(TemplateNameVar, CommonVariable.SourcePrinter);
            lstViewTemplatePrint.Items.Clear();

            int no = 1;
            foreach (var item in ListTemplatePrint)
            {

                ListViewItem lvi = new ListViewItem();
                lvi.Text = no.ToString();
                lvi.SubItems.Add(item.TemplateName);
                lvi.SubItems.Add(item.Source);
                if (item.Active == true) {
                    lvi.SubItems.Add("TRUE");

                } else {

                    lvi.SubItems.Add("FALSE");
                }

                if (item.PrintDefault == true) {
                    lvi.SubItems.Add("TRUE");

                } else {

                    lvi.SubItems.Add("FALSE");
                }
                lvi.SubItems.Add(item.Base64Logo);

                lstViewTemplatePrint.Items.Add(lvi);

                if (no == 500)
                {
                    Application.DoEvents();
                }
                no++;
            }
        }

        private void lstViewTemplatePrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstViewTemplatePrint.SelectedItems.Count == 0)
                return;

            string templateName = lstViewTemplatePrint.SelectedItems[0].SubItems[1].Text;
            TemplateName.Text = templateName;

            byte[] imageBytes = Convert.FromBase64String(lstViewTemplatePrint.SelectedItems[0].SubItems[5].Text);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                pictureBox1.Image = Image.FromStream(ms, true);
            }

        }

        private void Upload_Click(object sender, EventArgs e)
        {
            UploadFileTemplatePrint uploadFile = new UploadFileTemplatePrint();
            uploadFile.Show();
        }

        private async void PrintDefault_Click(object sender, EventArgs e)
        {
            if(TemplateName.Text == "")
            {
                MessageBox.Show("Nama Template Masih Kosong. Mohon Dicari Dahulu Datanya.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TemplateName.Focus();
                return;
            }

            HttpClient client = new HttpClient();
            GlobalObjectResponse res = new GlobalObjectResponse();
            string TemplateNameVar = "";

            TemplateNameVar = TemplateName.Text;

            var requestData = new { TemplateName = TemplateNameVar, Source = CommonVariable.SourcePrinter, CreatedBy = CommonVariable.CreatedBy, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            string json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CommonVariable.Token);

            var response = await client.PutAsync(CommonVariable.baseUrl + "/TemplatePrint/UpdateDefaultTemplatePrint", content);
            if (response.IsSuccessStatusCode) {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(response.StatusCode));
                MessageBox.Show(res.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } else {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(response.StatusCode));
                MessageBox.Show("Error UpdateDefaultTemplatePrint : " + res.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }
    }
}
