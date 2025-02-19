using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Desktop_Warranty_TSJ.Models;
using System.Text.Json;
using Desktop_Warranty_TSJ.Help;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Desktop_Warranty_TSJ
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            string exeDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string projectDirectory = Directory.GetParent(exeDirectory).Parent.Parent.FullName;

            //UrlPathFile.UrlTemplateBarcode = projectDirectory + "\\" + "TemplateBarcode" + "\\" + "RptBarcode.rdlc";
            //UrlPathFile.UrlTempFile = projectDirectory + "\\" + "TempFile";

            UrlPathFile.UrlTemplateBarcode = exeDirectory + "\\" + "TemplateBarcode" + "\\" + "RptQRCodeBusa.rdlc";
            UrlPathFile.UrlTempFile = exeDirectory + "\\" + "TempFile";

            UserID.Text = "";
            Password.Text = "";
            Password.PasswordChar = '*';

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string userId = UserID.Text;
            string password = Password.Text;

            if(userId == "" || userId == null)
            {
                MessageBox.Show("User Id masih kosong.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (password == "" || password == null)
            {
                MessageBox.Show("Password masih kosong.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var fromLogin = new Login();
            bool bolSuccess = await PostLoginAPI(userId, password);
            if(bolSuccess == true)
            {
                this.Hide();
                MenuPrint mp = new MenuPrint();
                mp.Show();
            }

        }

        public static async Task<bool> PostLoginAPI(string userId, string password)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            HttpClient client = new HttpClient();

            var requestData = new { UserId = userId, Password = password };
            string json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(CommonVariable.baseUrl + "/Login/LoginUserWeb", content);
            if (response.IsSuccessStatusCode) {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(response.StatusCode));

            } else  {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(response.StatusCode));
            }
          
            if(res.Error == false) {

                var data = JsonSerializer.Deserialize<LoginResponse>(JsonSerializer.Serialize(res.Data),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

                CommonVariable.Token = data.Token;
                CommonVariable.CreatedBy = userId;

                return true;

            } else {

                CommonVariable.Token = "";
                MessageBox.Show("Error : " + res.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
