using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDms.Models
{
    [Table("document_versions")]
    public class DocumentVersions
    {
        [Key]
        [Required]
        [Column("id")]
        public int id { get; set; }

        [Column("name")]
        [MaxLength(150)]
        public string? name { get; set; }

        [Column("user_id")]
        [MaxLength(50)]
        public string? user_id { get; set; }

        [Column("description")]
        [MaxLength(255)]
        public string? description { get; set; }

        [Column("file_path")]
        [MaxLength(255)]
        public string? file_path { get; set; }

        [Column("document_id")]
        [MaxLength(50)]
        public string? document_id { get; set; }

        [Column("created_at")]
        public DateTime created_at { get; set; }

        [Column("created_by")]
        [MaxLength(10)]
        public string? created_by { get; set; }

        [Column("updated_by")]
        [MaxLength(10)]
        public string? updated_by { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }

        [Column("extension")]
        [MaxLength(10)]
        public string? extension { get; set; }

        [Column("content_type")]
        [MaxLength(500)]
        public string? content_type { get; set; }

        [Column("encrypt_file")]
        public string? encrypt_file { get; set; }

        [Column("file_size")]
        public long? file_size { get; set; }

        [Column("version_number")]
        public int? version_number { get; set; }

        [Column("expired_date")]
        public DateTime expired_date { get; set; }
    }
}
