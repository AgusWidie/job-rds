using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDms.Models
{
    public class DocumentTags
    {

        public int id { get; set; }
        public string? user_id { get; set; }
        public string? document_id { get; set; }
        public string? tags_json { get; set; }
        public DateTime created_at { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
