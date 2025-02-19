using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDms.Models
{
    public class DocumentIndex
    {
        public int id { get; set; }
        public string? index_id { get; set; }
        public string? index_name { get; set; }
        public string? rules { get; set; }
        public string? index_value { get; set; }
        public string? document_type_id { get; set; }
        public int status { get; set; }
        public DateTime created_at { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? last_updated_at { get; set; }
        public string? updated_by { get; set; }
    }
}
