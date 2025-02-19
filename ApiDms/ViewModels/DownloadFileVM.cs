namespace ApiDms.ViewModels
{
    public class DownloadFileVM
    {
        public string? document_id { get; set; }
        public string? document_no { get; set; }
        public string? file_name { get; set; }
        public string? extension { get; set; }
        public string? content_type { get; set; }
        public string? file_path { get; set; }
        public string? encrypt_file { get; set; }
        public byte[]? decrypt_file { get; set; }
        public string? reference { get; set; }
        public DateTime? date_version { get; set; }
        public DateTime? expired_date { get; set; }

    }
}
