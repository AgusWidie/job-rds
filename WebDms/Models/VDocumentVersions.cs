using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDms.Models
{
    public class VDocumentVersions
    {
        public int id { get; set; }
        public string? document_id { get; set; }
        public string? document_name { get; set; }
        public int? version_number { get; set; }
        public string? name { get; set; }
        public int? file_size { get; set; }
        public string? path { get; set; }
        public string? user_id { get; set; }
    }
}
