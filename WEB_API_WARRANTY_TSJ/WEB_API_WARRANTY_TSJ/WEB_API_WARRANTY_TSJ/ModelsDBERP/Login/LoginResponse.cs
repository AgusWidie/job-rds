namespace WEB_API_WARRANTY_TSJ.ModelsDBERP.Login
{
    public class LoginResponse
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? RoleId { get; set; }
        public string? Role { get; set; }
        public string? GeneralId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? Message { get; set; }
        public DateTime? TokenExpired { get; set; }
    }
}
