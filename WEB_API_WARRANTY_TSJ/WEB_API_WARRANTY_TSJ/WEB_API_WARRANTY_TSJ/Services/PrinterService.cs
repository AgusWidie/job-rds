using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Drawing.Printing;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;
using WEB_API_WARRANTY_TSJ.Services.IService;

namespace WEB_API_WARRANTY_TSJ.Services
{
    public class PrinterService : IPrinterService
    {
        public readonly IConfiguration _configuration;
        public readonly DBWARContext _context;
        public readonly IErrorRepositories _errorRepositories;
        private IWebHostEnvironment _hostingEnvironment;
        private string filePath = "";

        public PrinterService(IConfiguration Configuration, DBWARContext context, IWebHostEnvironment hostingEnvironment, IErrorRepositories errorRepositories)
        {
            _configuration = Configuration;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _errorRepositories = errorRepositories;

        }

        //public async Task<GlobalObjectResponse> PrintQRCode(QrCode parameter, string PrinterName, CancellationToken cancellationToken)
        //{
        //    Microsoft.Reporting.WebForms.Warning[] warnings;
        //    string[] streamIds;
        //    string mimeType = string.Empty;
        //    string encoding = string.Empty;
        //    string extension = string.Empty;

        //    string fileName = "";
        //    string qrCode = "";
        //    string base64qrCode = "";

        //    GlobalObjectResponse res = new GlobalObjectResponse();
        //    LogError _addError = new LogError();

        //    try
        //    {
        //        qrCode = "" + parameter.QrCodeId + "|" + Convert.ToDateTime(parameter.CreatedAt).ToString("yyyy-MM-dd HH:mm:ss") + "|" + parameter.ActivationCode + "|" + parameter.RegistrationCode + "|TSJ";
        //        Bitmap barcode_qr_code = GenerateQRCode.ProcessQR(qrCode);
        //        base64qrCode = GenerateQRCode.Base64FromBitmap(barcode_qr_code);

        //        ReportViewer rptViewer = new ReportViewer();
        //        rptViewer.ProcessingMode = ProcessingMode.Local;
        //        rptViewer.LocalReport.ReportPath = UrlPathFile.filePathTemplateQRCode;
        //        rptViewer.LocalReport.EnableExternalImages = true;

        //        ReportParameterCollection reportparameter = new ReportParameterCollection();
        //        reportparameter.Add(new ReportParameter("Base64", base64qrCode, true));
        //        reportparameter.Add(new ReportParameter("ActivationCode", parameter.ActivationCode));
        //        reportparameter.Add(new ReportParameter("RegistrationCode", parameter.RegistrationCode));
        //        reportparameter.Add(new ReportParameter("CreatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        //        rptViewer.LocalReport.SetParameters(reportparameter);
        //        rptViewer.LocalReport.Refresh();
        //        byte[] bytes = rptViewer.LocalReport.Render("Image", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

        //        fileName = "QRCode_Warranty_New_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
        //        var pathTempFile = Path.Combine(UrlPathFile.filePathTempFile, fileName);
        //        filePath = pathTempFile;

        //        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        //        {
        //            fs.Write(bytes, 0, bytes.Length);
        //            fs.Flush();
        //            fs.Dispose();

        //        }

        //        try
        //        {
        //            PrintDocument pd = new PrintDocument();
        //            pd.PrinterSettings.PrinterName = PrinterName;
        //            pd.PrintPage += PrintPage;
        //            pd.PrinterSettings.Copies = 1;
        //            pd.Print();
        //            pd.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            res.Code = 500;
        //            res.Message = MessageRepositories.MessageFailed + " Print Document : " + ex.Message;
        //            res.Error = true;

        //            var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

        //            _addError.ServiceName = "PrintQRCode";
        //            _addError.ServiceError = res.Message;
        //            _addError.LogJson = jsonStr;
        //            _addError.CreatedBy = parameter.CreatedBy;
        //            _addError.ErrorDate = DateTime.Now;

        //            await _errorRepositories.AddLogError(_addError, cancellationToken);

        //            return res;
        //        }

        //        res.Code = 200;
        //        res.Message = MessageRepositories.MessageSuccess + " Print QR Code.";
        //        res.Error = false;
        //        return res;
        //    } 

        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        string jsonStr = "";
        //        if (ex.InnerException.Message != null)
        //        {
        //            res.Code = 500;
        //            res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
        //            res.Error = true;

        //            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

        //            _addError.ServiceName = "PrintQRCode";
        //            _addError.ServiceError = res.Message;
        //            _addError.LogJson = jsonStr;
        //            _addError.CreatedBy = parameter.CreatedBy;
        //            _addError.ErrorDate = DateTime.Now;

        //            await _errorRepositories.AddLogError(_addError, cancellationToken);

        //            return res;
        //        }
        //        res.Code = 500;
        //        res.Message = MessageRepositories.MessageError + " : " + ex.Message;
        //        res.Error = true;

        //        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

        //        _addError.ServiceName = "PrintQRCode";
        //        _addError.ServiceError = res.Message;
        //        _addError.LogJson = jsonStr;
        //        _addError.CreatedBy = parameter.CreatedBy;
        //        _addError.ErrorDate = DateTime.Now;

        //        await _errorRepositories.AddLogError(_addError, cancellationToken);

        //        return res;
        //    }

        //    catch (Exception ex)
        //    {
        //        string jsonStr = "";

        //        if (ex.InnerException.Message != null)
        //        {
        //            res.Code = 500;
        //            res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
        //            res.Error = true;

        //            jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

        //            _addError.ServiceName = "PrintQRCode";
        //            _addError.ServiceError = res.Message;
        //            _addError.LogJson = jsonStr;
        //            _addError.CreatedBy = parameter.CreatedBy;
        //            _addError.ErrorDate = DateTime.Now;

        //            await _errorRepositories.AddLogError(_addError, cancellationToken);

        //            return res;
        //        }
        //        res.Code = 500;
        //        res.Message = MessageRepositories.MessageError + " : " + ex.Message;
        //        res.Error = true;

        //        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

        //        _addError.ServiceName = "PrintQRCode";
        //        _addError.ServiceError = res.Message;
        //        _addError.LogJson = jsonStr;
        //        _addError.CreatedBy = parameter.CreatedBy;
        //        _addError.ErrorDate = DateTime.Now;

        //        await _errorRepositories.AddLogError(_addError, cancellationToken);

        //        return res;
        //    }


        //}

        public async Task<GlobalObjectResponse> PrintBarcodeSerialQR(BarcodeSerialQr parameter, string PrinterName, CancellationToken cancellationToken)
        {
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            string fileName = "";
            string serialCode = "";
            string qrCode = "";
            string base64qrCode = "";
            string base64serialCode = "";

            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();

            try
            {
                serialCode = "" + parameter.SerialQrId + "|" + Convert.ToDateTime(parameter.CreatedAt).ToString("yyyy-MM-dd HH:mm:ss") + "|" + parameter.SerialCode + "|TSJ";
                Bitmap barcode_serial_code = GenerateQRCode.ProcessQR(serialCode);
                base64serialCode = GenerateQRCode.Base64FromBitmap(barcode_serial_code);

                qrCode = "" + parameter.SerialQrId + "|" + Convert.ToDateTime(parameter.CreatedAt).ToString("yyyy-MM-dd HH:mm:ss") + "|" + parameter.RegistrationCode + "|TSJ";
                Bitmap barcode_qr_code = GenerateQRCode.ProcessQR(qrCode);
                base64qrCode = GenerateQRCode.Base64FromBitmap(barcode_qr_code);

                ReportViewer rptViewer = new ReportViewer();
                rptViewer.ProcessingMode = ProcessingMode.Local;
                rptViewer.LocalReport.ReportPath = UrlPathFile.filePathTemplateBarcode;
                rptViewer.LocalReport.EnableExternalImages = true;

                ReportParameterCollection reportparameter = new ReportParameterCollection();
                reportparameter.Add(new ReportParameter("Base64QRCode", base64qrCode, true));
                reportparameter.Add(new ReportParameter("Base64SerialCode", base64serialCode, true));
                reportparameter.Add(new ReportParameter("SerialCode", parameter.SerialCode));
                reportparameter.Add(new ReportParameter("RegistrationCode", parameter.RegistrationCode.ToUpper()));
                reportparameter.Add(new ReportParameter("CreatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                rptViewer.LocalReport.SetParameters(reportparameter);
                rptViewer.LocalReport.Refresh();
                byte[] bytes = rptViewer.LocalReport.Render("Image", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                fileName = "Serial_QR_Code_Warranty_New_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                var pathTempFile = Path.Combine(UrlPathFile.filePathTempFile, fileName);
                filePath = pathTempFile;

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                    fs.Dispose();

                }

                try
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings.PrinterName = PrinterName;
                    pd.PrintPage += PrintPage;
                    pd.PrinterSettings.Copies = 1;
                    pd.Print();
                    pd.Dispose();
                }
                catch (Exception ex)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageFailed + " Print Document : " + ex.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "PrintBarcodeSerialQR";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Print barcode Serial QR.";
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "PrintBarcodeSerialQR";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "PrintBarcodeSerialQR";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.CreatedBy;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

            catch (Exception ex)
            {
                string jsonStr = "";

                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "PrintBarcodeSerialQR";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "PrintBarcodeSerialQR";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.CreatedBy;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }


        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);
            //Rectangle destRect = new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(width), Convert.ToInt32(height));
            System.Drawing.Point loc = new System.Drawing.Point(0, 0);
            e.Graphics.DrawImage(img, loc);
            img.Dispose();
        }

        public async Task<GlobalObjectResponse> PrintActivationQR(ActivationQr parameter, string PrinterName, CancellationToken cancellationToken)
        {
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            string fileName = "";
            string qrCode = "";
            string base64qrCode = "";

            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();

            try
            {

                qrCode = "" + DateTime.Now.Ticks.ToString("x").ToUpper() + "|" + Convert.ToDateTime(parameter.CreatedAt).ToString("yyyy-MM-dd HH:mm:ss") + "|" + parameter.ActivationCode + "|TSJ";
                Bitmap barcode_qr_code = GenerateQRCode.ProcessQR(qrCode);
                base64qrCode = GenerateQRCode.Base64FromBitmap(barcode_qr_code);

                ReportViewer rptViewer = new ReportViewer();
                rptViewer.ProcessingMode = ProcessingMode.Local;
                rptViewer.LocalReport.ReportPath = UrlPathFile.filePathTemplateActivationCode;
                rptViewer.LocalReport.EnableExternalImages = true;

                ReportParameterCollection reportparameter = new ReportParameterCollection();
                reportparameter.Add(new ReportParameter("Base64QRCode", base64qrCode, true));
                reportparameter.Add(new ReportParameter("ActivationCode", parameter.ActivationCode.ToUpper()));
                reportparameter.Add(new ReportParameter("CreatedAt", Convert.ToDateTime(parameter.CreatedAt).ToString("yyyy-MM-dd HH:mm:ss")));
                rptViewer.LocalReport.SetParameters(reportparameter);
                rptViewer.LocalReport.Refresh();
                byte[] bytes = rptViewer.LocalReport.Render("Image", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                fileName = "Activation_QR_Code_Warranty_New_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                var pathTempFile = Path.Combine(UrlPathFile.filePathTempFile, fileName);
                filePath = pathTempFile;

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Flush();
                    fs.Dispose();

                }

                try
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings.PrinterName = PrinterName;
                    pd.PrintPage += PrintPage;
                    pd.PrinterSettings.Copies = 1;
                    pd.Print();
                    pd.Dispose();
                }
                catch (Exception ex)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageFailed + " Print Document : " + ex.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "PrintActivationQR";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Print barcode Serial QR.";
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "PrintActivationQR";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "PrintActivationQR";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.CreatedBy;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

            catch (Exception ex)
            {
                string jsonStr = "";

                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "PrintActivationQR";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "PrintActivationQR";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.CreatedBy;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }


        }
    }
}
