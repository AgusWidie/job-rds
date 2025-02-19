namespace WEB_API_WARRANTY_TSJ.Models.Warranty
{
    public class WarrantyRequest
    {
        public string? SerialCode { get; set; }
        public string? ActivationCode { get; set; }
        public string? RegistrationCode { get; set; }
        public string? QrCodeFull { get; set; }
        public string? CreatedBy { get; set; }
    }
}
