namespace WebDms.ViewModels
{
    public class Response
    {
        public int code { get; set; }
        public bool status { get; set; }
        public string? message { get; set; }
    }

    public class ResponseListData
    {
        public int code { get; set; }
        public bool error { get; set; }
        public string? message { get; set; }
        public List<Object>? data{ get; set; }
    }

    public class ResponseData
    {
        public int code { get; set; }
        public bool error { get; set; }
        public string? message { get; set; }
        public Object? data { get; set; }
    }
}
