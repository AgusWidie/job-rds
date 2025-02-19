namespace ApiBarangBukti.ViewModel
{
    public class UploadFileVM
    {
        public string? file_name { get; set; }

        public string? content_type { get; set; }

        public string? extension { get; set; }

        public long? file_size { get; set; }

        public string? base64file { get; set; }
    }
}
