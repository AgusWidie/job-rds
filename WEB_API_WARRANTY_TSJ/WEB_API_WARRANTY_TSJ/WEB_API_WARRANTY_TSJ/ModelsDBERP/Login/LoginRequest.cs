namespace WEB_API_WARRANTY_TSJ.ModelsDBERP.Login
{
    public class LoginRequest
    {
        public string? Company { get; set; }
        public string? Platform { get; set; }
        public string? UserId { get; set; }
        public string? WebId { get; set; }
        public string? Telepon { get; set; }
        public string? Password { get; set; }
    }
}
