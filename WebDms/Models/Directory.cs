using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDms.Models
{
    [Table("directories")]
    public class Directory
    {
        public int id { get; set; }
        public string? directory_id { get; set; }
        public string? directory_name { get; set; }
        public string? disk { get; set; }
        public string? path_name { get; set; }
        public int parent_id { get; set; }
        public string? collection_id { get; set; }
        public string? owner_id { get; set; }
        public int status { get; set; }
        public DateTime created_at { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? last_updated_at { get; set; }
        public string? updated_by { get; set; }
    }
}
