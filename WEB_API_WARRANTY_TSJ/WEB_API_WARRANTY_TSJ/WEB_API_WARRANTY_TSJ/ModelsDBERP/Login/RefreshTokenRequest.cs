namespace WEB_API_WARRANTY_TSJ.ModelsDBERP.Login
{
    public class RefreshTokenRequest
    {
        public string? UserId { get; set; }
        public string? Telepon { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
