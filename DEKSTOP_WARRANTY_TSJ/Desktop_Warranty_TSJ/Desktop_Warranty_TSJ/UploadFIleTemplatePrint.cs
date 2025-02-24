using Desktop_Warranty_TSJ.Help;
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

namespace Desktop_Warranty_TSJ
{
    public partial class UploadFileTemplatePrint : Form
    {
        public static string FileBase64 = "";
        public static byte[] mOriginalData, mConvertedData;
        public UploadFileTemplatePrint()
        {
            InitializeComponent();
        }

        private void SelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog opDialog = new OpenFileDialog();
            opDialog.Multiselect = false;
            opDialog.ShowDialog();
            opDialog.Filter = "Jpeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            textBox1.Text = opDialog.FileName;

            if (opDialog.FileName != "")
            {

                byte[] data = File.ReadAllBytes(opDialog.FileName);
                FileBase64 = Convert.ToBase64String(data);
             
            }
        }

        private void UploadFileTemplatePrint_Load(object sender, EventArgs e)
        {
            this.AutoSize = false;
        }

        private async void SaveFile_Click(object sender, EventArgs e)
        {
            if (FileBase64 == "")
            {
                MessageBox.Show("Upload File Masih Kososng.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (TemplateName.Text == "")
            {
                MessageBox.Show("Nama Template Masih Kososng.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            HttpClient client = new HttpClient();
            GlobalObjectResponse res = new GlobalObjectResponse();

            string templateName = TemplateName.Text;

            var requestData = new { TemplateName = templateName, Base64Logo = FileBase64, Source = CommonVariable.SourcePrinter, Active = true, PrintDefault = false, CreatedBy = CommonVariable.CreatedBy, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
            string json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CommonVariable.Token);
            var response = await client.PostAsync(CommonVariable.baseUrl + "/TemplatePrint/AddTemplatePrint", content);

            if (response.IsSuccessStatusCode) {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(response.StatusCode));
                MessageBox.Show(res.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FileBase64 = "";

            } else {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(response.StatusCode));
                MessageBox.Show("Error : " + res.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
