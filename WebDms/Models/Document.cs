using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDms.Models
{
    public class Document
    {
        public int id { get; set; }
        public string? document_id { get; set; }
        public string? file_name { get; set; }
        public string? content_type { get; set; }
        public string? extension { get; set; }
        public string? file_path { get; set; }
        public string? collection_id { get; set; }
        public string? document_type_id { get; set; }
        public string? directory_id { get; set; }
        public int version { get; set; }
        public long file_size { get; set; }
        public int download { get; set; }
        public string? owner_id { get; set; }
        public DateTime? expired_date { get; set; }
        public int status { get; set; }
        public DateTime created_at { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? last_updated_at { get; set; }
        public string? updated_by { get; set; }
        public string? document_no { get; set; }
        public string? document_name { get; set; }
        public string? reference { get; set; }
        public DateTime? date_version { get; set; }
        public string? encrypt_file { get; set; }
        public DateTime? download_date { get; set; }
    }
}
