using Desktop_Warranty_TSJ.Help;
using Desktop_Warranty_TSJ.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq;

namespace Desktop_Warranty_TSJ
{
    public partial class MenuPrint : Form
    {
        public static bool boolCheckSelectDate = false; 
        public MenuPrint()
        {
            InitializeComponent();
        }

        private void MenuPrint_Load(object sender, EventArgs e)
        {
            SelectDate.Checked = false;
            label2.Visible = false;
            label3.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;

            // Inisialisasi ListView
            lstViewSerialCode.View = View.Details;  // Mengatur tampilan menjadi mode detail agar ada header kolom
            lstViewSerialCode.FullRowSelect = true;  // Memilih seluruh baris saat diklik
            lstViewSerialCode.GridLines = true;      // Menampilkan garis pembatas antar baris
            lstViewSerialCode.Scrollable = true; // Enable scrolling
            lstViewSerialCode.MultiSelect = true;

            // Menambahkan kolom (header) ke ListView
            lstViewSerialCode.Columns.Add("No.", 50);   
            lstViewSerialCode.Columns.Add("Serial Number", 150);
            lstViewSerialCode.Columns.Add("Registration Code", 150);
            lstViewSerialCode.Columns.Add("Created By", 120);
            lstViewSerialCode.Columns.Add("Created Date", 120);

            SelectDate.Checked = false;
        }

        private async void Cari_Click(object sender, EventArgs e)
        {


            string serialCode = "";
            DateTime? createdAtFrom = null;
            DateTime? createdAtTo = null;
            bool selectDate = false;
            List<BarcodeSerialQr> ListBarcodeSerial = new List<BarcodeSerialQr>();

            serialCode = SerialCode.Text;
            createdAtFrom = dateTimePicker1.Value;
            createdAtTo = dateTimePicker2.Value;

            if (SelectDate.Checked == false) {
                selectDate = false;
            } else {
                selectDate = true;
            }

            if(selectDate == true)
            {
                if (createdAtTo.Value.Date < createdAtFrom.Value.Date)  {

                    MessageBox.Show("Sampai Tanggal Lebih Besar Dari Tanggal.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ListBarcodeSerial = await GetDataPrintSerialCode(serialCode, selectDate, createdAtFrom, createdAtTo);
            if (ListBarcodeSerial.Count() <= 0)
            {
                lstViewSerialCode.Items.Clear();
                MessageBox.Show("Data Serial Number Tidak Ada.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            lstViewSerialCode.Items.Clear();

            int no = 1;
            foreach (var item in ListBarcodeSerial)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = no.ToString();
                lvi.SubItems.Add(item.SerialCode);
                lvi.SubItems.Add(item.RegistrationCode);
                lvi.SubItems.Add(item.CreatedBy);
                lvi.SubItems.Add(Convert.ToDateTime(item.CreatedAt).ToString("yyyy-MM-dd"));

                lstViewSerialCode.Items.Add(lvi);

                if (no == 500) {
                    Application.DoEvents();
                }
                no++;
            }
        }

        public static async Task<List<BarcodeSerialQr>> GetDataPrintSerialCode(string SerialCode, bool SelectDate, DateTime? CreatedAtFrom, DateTime? CreatedAtTo)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            HttpClient client = new HttpClient();
            List<BarcodeSerialQr> resData = new List<BarcodeSerialQr>();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CommonVariable.Token);
            var response = await client.GetAsync(CommonVariable.baseUrl + "/BarcodeSerialQR/ListDataBarcodeSerialQR?SerialCode=" + SerialCode + "&Source=" + CommonVariable.SourcePrinter + "&SelectDate=" + SelectDate.ToString() + "&CreatedAtFrom=" + Convert.ToDateTime(CreatedAtFrom).ToString("yyyy-MM-dd") + "&CreatedAtTo=" + Convert.ToDateTime(CreatedAtTo).ToString("yyyy-MM-dd"));
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
                var dataList = JsonSerializer.Deserialize<List<BarcodeSerialQr>>(JsonSerializer.Serialize(res.Data),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });

                resData = dataList;
                return resData;

            }  else {

                MessageBox.Show("Error GetDataPrintSerialCode : " + res.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return resData;
            }
        }

        private void ViewPrint_Click(object sender, EventArgs e)
        {
            Print print = new Print();
            print.Show();
        }

        
  
        private void MenuPrint_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void SelectDate_Click(object sender, EventArgs e)
        {
            if (SelectDate.Checked == true) {
                boolCheckSelectDate = true;

            } else {

                boolCheckSelectDate = false;
            }

            if (boolCheckSelectDate == true) {

                label2.Visible = true;
                label3.Visible = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;

                SerialCode.Text = "";
                SerialCode.Enabled = false;

            }  else {

                label2.Visible = false;
                label3.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;

                SerialCode.Text = "";
                SerialCode.Enabled = true;
            }
        }

        public static string FilePath = "";
        private async void PrintData_Click(object sender, EventArgs e)
        {
           
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string printerName = "";

            HttpClient client = new HttpClient();
            GlobalObjectResponse res = new GlobalObjectResponse();
            GlobalObjectListResponse resPrinter = new GlobalObjectListResponse();
            GlobalObjectListResponse resTemplatePrint = new GlobalObjectListResponse();

            string urlAddBarcodeSerialQR = "";
            string urlAPI = "";
            string printerValue = "";

            if (CommonVariable.SourcePrinter == "TSJ")
            {
                urlAddBarcodeSerialQR = "AddBarcodeSerialQRTSJ";
                printerValue = "PrinterTSJ";
            }

            if (CommonVariable.SourcePrinter == "INOAC")
            {
                urlAddBarcodeSerialQR = "AddBarcodeSerialQRINOAC";
                printerValue = "PrinterINOAC";
            }

            
            try
            {
                string serialCodeVar = "";
                string serialQrId = DateTime.Now.Ticks.ToString("x").ToUpper();
                string serialCode = DateTime.Now.AddSeconds(1).Ticks.ToString("x").ToUpper();
                string registrationCode = DateTime.Now.AddSeconds(2).Ticks.ToString("x").ToUpper();
                string base64serialCode = "";
                string qrCode = "";
                string base64qrCode = "";
                string base64Logo = "";

                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CommonVariable.Token);
                //var responsePrinter = await client.GetAsync(CommonVariable.baseUrl + "/Printer/GetPrinterName?PrinterValue=" + printerValue + "");
                //if (responsePrinter.IsSuccessStatusCode) {

                //    string responseContent = await responsePrinter.Content.ReadAsStringAsync();
                //    resPrinter = ResponseAPI.ResponseListSuccessAPI(responseContent, Convert.ToInt32(responsePrinter.StatusCode));

                //    var dataPrinter = JsonSerializer.Deserialize<List<SettingPrinter>>(JsonSerializer.Serialize(res.Data),
                //    new JsonSerializerOptions
                //    {
                //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                //        PropertyNameCaseInsensitive = true
                //    });

                //    if (dataPrinter != null) {
                //        printerName = dataPrinter.FirstOrDefault().PrinterName;

                //    } else {

                //        MessageBox.Show("Printer Name Masih Kosong.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }

                //} else {

                //    string responseContent = await responsePrinter.Content.ReadAsStringAsync();
                //    resPrinter = ResponseAPI.ResponseListErrorAPI(responseContent, Convert.ToInt32(responsePrinter.StatusCode));

                //    MessageBox.Show("Error : " + resPrinter.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CommonVariable.Token);
                var responsePrinter = await client.GetAsync(CommonVariable.baseUrl + "/TemplatePrint/GetDataDefaultTemplatePrint?Source=" + CommonVariable.SourcePrinter + "");
                if (responsePrinter.IsSuccessStatusCode) {

                    string responseContent = await responsePrinter.Content.ReadAsStringAsync();
                    resTemplatePrint = ResponseAPI.ResponseListSuccessAPI(responseContent, Convert.ToInt32(responsePrinter.StatusCode));

                    var dataTemp = JsonSerializer.Deserialize<List<TemplatePrint>>(JsonSerializer.Serialize(resTemplatePrint.Data),
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true
                    });

                    if (dataTemp != null) {
                        base64Logo = dataTemp.FirstOrDefault().Base64Logo;

                    } else {

                        base64Logo = " ";
                        //MessageBox.Show("Template Print Logo Masih Kosong.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //return;
                    }

                } else {

                    //string responseContent = await responsePrinter.Content.ReadAsStringAsync();
                    //resPrinter = ResponseAPI.ResponseListErrorAPI(responseContent, Convert.ToInt32(responsePrinter.StatusCode));

                    //MessageBox.Show("Error : " + resPrinter.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //return;

                    base64Logo = " ";
                }

                var requestData = new { SerialQrId = serialQrId, SerialCode = serialCode, RegistrationCode = registrationCode, Source = CommonVariable.SourcePrinter, TotalPrint = 1, CreatedBy = CommonVariable.CreatedBy, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                string json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(CommonVariable.baseUrl + "/BarcodeSerialQR/" + urlAddBarcodeSerialQR + "", content);

                if (response.IsSuccessStatusCode) {

                    string responseContent = await response.Content.ReadAsStringAsync();
                    res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(response.StatusCode));

                } else {

                    string responseContent = await response.Content.ReadAsStringAsync();
                    res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(response.StatusCode));
                }

                //print
                serialCodeVar = "www.vita-foam.com/warranty/" + serialCode.ToUpper() + "|" + serialCode + "|" + CommonVariable.SourcePrinter + "";
                Bitmap barcode_serial_code = GenerateQRCode.GenerateBarcode(serialCodeVar);
                base64serialCode = GenerateQRCode.Base64FromBitmap(barcode_serial_code);

                qrCode = "www.vita-foam.com/warranty/" + registrationCode.ToUpper() + "|" + registrationCode + "|" + CommonVariable.SourcePrinter + "";
                Bitmap barcode_qr_code = GenerateQRCode.ProcessQR(qrCode);
                base64qrCode = GenerateQRCode.Base64FromBitmap(barcode_qr_code);

                ReportViewer rptViewer = new ReportViewer();
                rptViewer.ProcessingMode = ProcessingMode.Local;
                rptViewer.LocalReport.ReportPath = UrlPathFile.UrlTemplateBarcode;
                rptViewer.LocalReport.EnableExternalImages = true;

                ReportParameterCollection reportparameter = new ReportParameterCollection();
                reportparameter.Add(new ReportParameter("Base64QRCode", base64qrCode, true));
                reportparameter.Add(new ReportParameter("Base64SerialCode", base64serialCode, true));
                reportparameter.Add(new ReportParameter("Base64Logo", base64Logo, true));
                reportparameter.Add(new ReportParameter("SerialCode", serialCode.ToUpper()));
                reportparameter.Add(new ReportParameter("RegistrationCode", "www.vita-foam.com/warranty/" + registrationCode.ToUpper()));
                reportparameter.Add(new ReportParameter("CreatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                rptViewer.LocalReport.SetParameters(reportparameter);
                rptViewer.SizeToReportContent = true;

                rptViewer.LocalReport.Refresh();

                byte[] bytes = rptViewer.LocalReport.Render("image", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                string FileName = "BarcodeSerialQR_" + CommonVariable.SourcePrinter + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                FilePath = UrlPathFile.UrlTempFile;
                FilePath = FilePath + "\\" + FileName;

                using (FileStream fs = new FileStream(FilePath, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                    fs.Dispose();
                }

                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = printerName;
                pd.PrintPage += PrintPage;
                pd.PrinterSettings.Copies = 1;
                pd.Print();
                pd.Dispose();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                MessageBox.Show("Error Print : " + message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(FilePath);
            System.Drawing.Point loc = new System.Drawing.Point(100, 100);
            e.Graphics.DrawImage(img, loc);
            img.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TemplateDataPrint temPrint = new TemplateDataPrint();
            temPrint.Show();
        }

        private async void Hapus_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            GlobalObjectResponse res = new GlobalObjectResponse();
            string SerialCodeVar = "";

            if (SerialCode.Text == "") {

                MessageBox.Show("Serial Number Masih Kosong. Mohon Dicari Dahulu Datanya.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SerialCode.Focus();
                return;
            }

            SerialCodeVar = SerialCode.Text;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CommonVariable.Token);
            var response = await client.DeleteAsync(CommonVariable.baseUrl + "/BarcodeSerialQR/DeleteBarcodeSerialQR?SerialCode=" + SerialCodeVar.Trim() + "&Source=" + CommonVariable.SourcePrinter);
            if (response.IsSuccessStatusCode) {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(response.StatusCode));

            }  else {

                string responseContent = await response.Content.ReadAsStringAsync();
                res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(response.StatusCode));
                MessageBox.Show("Error DeleteBarcodeSerialQR : " + res.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string serialCode = "";
            DateTime? createdAtFrom = null;
            DateTime? createdAtTo = null;
            bool selectDate = false;
            List<BarcodeSerialQr> ListBarcodeSerial = new List<BarcodeSerialQr>();

            serialCode = SerialCode.Text;
            createdAtFrom = dateTimePicker1.Value;
            createdAtTo = dateTimePicker2.Value;

            if (SelectDate.Checked == false) {
                selectDate = false;
            } else {
                selectDate = true;
            }

            if (selectDate == true) {
                if (createdAtTo.Value.Date < createdAtFrom.Value.Date) {

                    MessageBox.Show("Sampai Tanggal Lebih Besar Dari Tanggal.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            ListBarcodeSerial = await GetDataPrintSerialCode(serialCode, selectDate, createdAtFrom, createdAtTo);
            lstViewSerialCode.Items.Clear();

            int no = 1;
            foreach (var item in ListBarcodeSerial)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = no.ToString();
                lvi.SubItems.Add(item.SerialCode);
                lvi.SubItems.Add(item.RegistrationCode);
                lvi.SubItems.Add(item.CreatedBy);
                lvi.SubItems.Add(Convert.ToDateTime(item.CreatedAt).ToString("yyyy-MM-dd"));

                lstViewSerialCode.Items.Add(lvi);

                if (no == 500)
                {
                    Application.DoEvents();
                }
                no++;
            }
        }

        private void lstViewSerialCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstViewSerialCode.SelectedItems.Count == 0)
                return;

            string serialCode = this.lstViewSerialCode.SelectedItems[1].Text;
            SerialCode.Text = serialCode;
        }
    }
}
