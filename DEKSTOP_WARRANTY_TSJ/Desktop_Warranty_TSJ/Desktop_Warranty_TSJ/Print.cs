using Desktop_Warranty_TSJ.Help;
using Desktop_Warranty_TSJ.Models;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using Warning = Microsoft.Reporting.WebForms.Warning;

namespace Desktop_Warranty_TSJ
{
    public partial class Print : Form
    {
        public Print()
        {
            InitializeComponent();
        }

        public static string FilePath = "";
        private void Print_Load(object sender, EventArgs e)
        {
            Message.Text = "";
            Message.Visible = false;

            TotalPrint.Text = "0";
            ProsesData.Text = TotalPrint.Text;
            ProsesData.Enabled = false;
            SourcePrinter.Text = CommonVariable.SourcePrinter;

            progressBar1.Value = 0;
            this.AutoSize = false;
        }

        private async void PrintSerialNumber_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Anda Yakin Ingin Print Barcode ?", "Informasi", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string printerName = "";

                Message.Visible = false;
                Message.Text = "";

                HttpClient client = new HttpClient();
                GlobalObjectResponse res = new GlobalObjectResponse();
                GlobalObjectListResponse resPrinter = new GlobalObjectListResponse();

                Message.Text = "";
                ProsesData.Text = TotalPrint.Text;
                ProsesData.Enabled = false;

                string urlAddBarcodeSerialQR = "";
                string urlAPI = "";
                string printerValue = "";

                if (CommonVariable.SourcePrinter == "TSJ") {
                    urlAddBarcodeSerialQR = "AddBarcodeSerialQRTSJ";
                    printerValue = "PrinterTSJ";
                }

                if (CommonVariable.SourcePrinter == "INOAC") {
                    urlAddBarcodeSerialQR = "AddBarcodeSerialQRINOAC";
                    printerValue = "PrinterINOAC";
                }

                string sProsesData = ProsesData.Text;
                int iProsesData = 0;

                bool success = int.TryParse(sProsesData, out iProsesData);
                if (success == false)
                {
                    //MessageBox.Show("Informasi : Total Print Bukan Tipe Angka/Integer.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Message.Visible = true;
                    Message.Text = "Total Print Bukan Tipe Angka/Integer.";
                    Message.ForeColor = Color.Red;
                    return;
                }

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

                //        Message.Visible = true;
                //        Message.Text = "Printer Name tidak ada. Printer Value : " + printerValue + "";
                //        Message.ForeColor = Color.Red;
                //        return;
                //    }

                //} else {

                //    string responseContent = await responsePrinter.Content.ReadAsStringAsync();
                //    resPrinter = ResponseAPI.ResponseListErrorAPI(responseContent, Convert.ToInt32(responsePrinter.StatusCode));

                //    MessageBox.Show("Error : " + resPrinter.Message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
               
                progressBar1.Value = 0;
                progressBar1.Maximum = iProsesData;

                try
                {
                    int i = 1;
                    while (iProsesData > 0)
                    {
                        string serialCodeVar = "";
                        string serialQrId = DateTime.Now.Ticks.ToString("x").ToUpper();
                        string serialCode = DateTime.Now.AddSeconds(1).Ticks.ToString("x").ToUpper();
                        string registrationCode = DateTime.Now.AddSeconds(2).Ticks.ToString("x").ToUpper();
                        string base64serialCode = "";
                        string qrCode = "";
                        string base64qrCode = "";

                        var requestData = new { SerialQrId = serialQrId, SerialCode = serialCode, RegistrationCode = registrationCode, Source = CommonVariable.SourcePrinter, TotalPrint = 1, CreatedBy = CommonVariable.CreatedBy, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
                        string json = JsonSerializer.Serialize(requestData);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + CommonVariable.Token);
                        var response = await client.PostAsync(CommonVariable.baseUrl + "/BarcodeSerialQR/" + urlAddBarcodeSerialQR + "", content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(response.StatusCode));

                        }
                        else
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(response.StatusCode));
                        }

                        if (res.Error == true)
                        {
                            Message.Visible = true;
                            Message.Text = res.Message;
                            Message.ForeColor = Color.Red;
                            break;
                        }

                        progressBar1.Value = progressBar1.Value + 1;
                        iProsesData = iProsesData - 1;
                        ProsesData.Text = iProsesData.ToString();

                        if (i == 100)
                        {
                            Application.DoEvents();
                        }

                        //print
                        serialCodeVar = "" + serialQrId + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + serialCode + "|" + CommonVariable.SourcePrinter + "";
                        Bitmap barcode_serial_code = GenerateQRCode.GenerateBarcode(serialCodeVar);
                        base64serialCode = GenerateQRCode.Base64FromBitmap(barcode_serial_code);

                        qrCode = "" + serialQrId + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "|" + registrationCode + "|" + CommonVariable.SourcePrinter + "";
                        Bitmap barcode_qr_code = GenerateQRCode.ProcessQR(qrCode);
                        base64qrCode = GenerateQRCode.Base64FromBitmap(barcode_qr_code);

                        ReportViewer rptViewer = new ReportViewer();
                        rptViewer.ProcessingMode = ProcessingMode.Local;
                        rptViewer.LocalReport.ReportPath = UrlPathFile.UrlTemplateBarcode;
                        rptViewer.LocalReport.EnableExternalImages = true;

                        ReportParameterCollection reportparameter = new ReportParameterCollection();
                        reportparameter.Add(new ReportParameter("Base64QRCode", base64qrCode, true));
                        reportparameter.Add(new ReportParameter("Base64SerialCode", base64serialCode, true));
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

                        //////////////////////////

                        i++;
                    }
                }
                catch(Exception ex)
                {
                    string message = ex.Message;
                    MessageBox.Show("Error Print : " + message, "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
           
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(FilePath);
            System.Drawing.Point loc = new System.Drawing.Point(100, 100);
            e.Graphics.DrawImage(img, loc);
            img.Dispose();
        }

        
   
    }
}
