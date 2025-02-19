namespace WebDms.ViewModels
{
    public class DocumentVersionVM
    {
        public int id { get; set; }
        public string? directory_id { get; set; }
        public string? document_id { get; set; }
        public string? document_name { get; set; }
        public int? version_number { get; set; }
        public string? name { get; set; }
        public int? file_size { get; set; }
        public string? file_path { get; set; }
        public string? user_id { get; set; }
        public string? created_at { get; set; }
        public string? extension { get; set; }
        public string? content_type { get; set; }
        public DateTime expired_date { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
