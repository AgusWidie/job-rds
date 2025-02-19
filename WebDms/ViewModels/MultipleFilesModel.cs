namespace WebDms.ViewModels
{
    public class MultipleFilesModel
    {
        public int? id { get; set; }
        public string? collection_id { get; set; }
        public string? directory_id { get; set; }
        public string? file_name { get; set; }
        public string? document_type_id { get; set; }
        public string? reference { get; set; }
        public string? DocumentName { get; set; }
        public string? document_name { get; set; }
        public string? document_no { get; set; }
        public DateTime? date_version { get; set; }
        public DateTime? expired_date { get; set; }
        public List<IFormFile> files { get; set; }
        public string? index_value { get; set; }
        public string? document_tag { get; set; }
    }
}
