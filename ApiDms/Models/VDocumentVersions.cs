using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiDms.Models
{
    [Table("v_document_versions")]
    public class VDocumentVersions
    {
        [Key]
        [Required]
        [Column("id")]
        public int id { get; set; }

        [Column("document_id")]
        [MaxLength(50)]
        public string? document_id { get; set; }

        [Column("document_name")]
        [MaxLength(150)]
        public string? document_name { get; set; }

        [Column("version_number")]
        public int? version_number { get; set; }

        [Column("name")]
        [MaxLength(150)]
        public string? name { get; set; }

        [Column("file_size")]
        public int? file_size { get; set; }

        [Column("file_path")]
        [MaxLength(255)]
        public string? file_path { get; set; }

        [Column("user_id")]
        [MaxLength(50)]
        public string? user_id { get; set; }

        [Column("extension")]
        [MaxLength(50)]
        public string? extension { get; set; }

        [Column("created_at")]
        public DateTime created_at { get; set; }

    }
}
