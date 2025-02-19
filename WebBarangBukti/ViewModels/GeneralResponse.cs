namespace WebBarangBukti.ViewModels
{
    public class GeneralResponse
    {
        public int code { get; set; }
        public bool status { get; set; }
        public string? message { get; set; }
        public string? data { get; set; }
    }
}
