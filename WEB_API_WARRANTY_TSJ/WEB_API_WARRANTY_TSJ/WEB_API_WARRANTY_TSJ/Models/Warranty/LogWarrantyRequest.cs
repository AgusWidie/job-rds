namespace WEB_API_WARRANTY_TSJ.Models.Warranty
{
    public class LogWarrantyRequest
    {
        public string? SerialCode { get; set; }
        public string? ActivationCode { get; set; }
        public string? RegistrationCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Email { get; set; }
        public string? PlaceOfBirth { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Telephone { get; set; }
        public string? Additional1 { get; set; }
        public string? Additional2 { get; set; }
        public string? Additional3 { get; set; }
        public string? Additional4 { get; set; }
        public string? Additional5 { get; set; }
        public string? CreatedBy { get; set; }
    }
}
