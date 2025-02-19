namespace WebDms.ViewModels
{
    public class DocumentIndexValueVM
    {
        public int id { get; set; }
        public string? document_type_id { get; set; }
        public string? index_id { get; set; }
        public string? index_value { get; set; }
        public DateTime created_at { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? last_updated_at { get; set; }
        public string? updated_by { get; set; }
    }
}
