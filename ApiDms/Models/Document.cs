using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDms.Models
{
    [Table("documents")]
    public class Document
    {
        [Key]
        [Required]
        [Column("id")]
        public int id { get; set; }

        [Column("document_id")]
        [MaxLength(50)]
        public string? document_id { get; set; }

        [Column("file_name")]
        public string? file_name { get; set; }

        [Column("content_type")]
        [MaxLength(255)]
        public string? content_type { get; set; }
        [Column("extension")]
        [MaxLength(10)]
        public string? extension { get; set; } 

        [Column("file_path")]
        public string? file_path { get; set; }

        [Column("collection_id")]
        [MaxLength(50)]
        public string? collection_id { get; set; }

        [Column("document_type_id")]
        [MaxLength(50)]
        public string? document_type_id { get; set; }

        [Column("directory_id")]
        [MaxLength(50)]
        public string? directory_id { get; set; }

        [Column("version")]
        public int version { get; set; }

        [Column("file_size")]
        public long file_size { get; set; }

        [Column("download")]
        public int download { get; set; }

        [Column("owner_id")]
        [MaxLength(10)]
        public string? owner_id { get; set; }

        [Column("expired_date")]
        public DateTime? expired_date { get; set; }

        [Column("status")]
        public int status { get; set; }

        [Column("created_at")]
        public DateTime created_at { get; set; }

        [Column("created_by")]
        [MaxLength(10)]
        public string? created_by { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }

        [Column("last_updated_at")]
        public DateTime? last_updated_at { get; set; }

        [Column("updated_by")]
        [MaxLength(10)]
        public string? updated_by { get; set; }

        [Column("document_no")]
        [MaxLength(50)]
        public string? document_no { get; set; }

        [Column("document_name")]
        [MaxLength(50)]
        public string? document_name { get; set; }

        [Column("reference")]
        [MaxLength(50)]
        public string? reference { get; set; }

        [Column("date_version")]
        public DateTime? date_version { get; set; }

        [Column("encrypt_file")]
        public string? encrypt_file { get; set; }

        [Column("download_date")]
        public DateTime? download_date { get; set; }

    }
}
