namespace WebDms.ViewModels
{
    public class UploadFileVM
    {
        public int? id { get; set; }
        public string? directory_id { get; set; }
        public string? document_type_id { get; set; }
        public string? index_id { get; set; }
        public string? file_name { get; set; }
        public string? content_type { get; set; }
        public string? extension { get; set; }
        public string? base64file { get; set; }
        public long file_size { get; set; }
        public string? collection_id { get; set; }
        public string? reference { get; set; }
        public string? document_id { get; set; }
        public string? document_name { get; set; }
        public string? document_no { get; set; }
        public DateTime? date_version { get; set; }
        public DateTime? expired_date { get; set; }
        public string? created_by { get; set; }
        public byte[]? result_encrypt_sha { get; set; }
        public string? index_value { get; set; }
        public string? document_tag { get; set; }
    }
}
