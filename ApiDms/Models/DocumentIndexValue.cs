using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiDms.Models
{
    [Table("document_indices_value")]
    public class DocumentIndexValue
    {
        [Key]
        [Required]
        [Column("id")]
        public int id { get; set; }
        [Column("document_type_id")]
        [MaxLength(50)]
        public string? document_type_id { get; set; }
        [Column("document_id")]
        [MaxLength(50)]
        public string? document_id { get; set; }
        [Column("index_id")]
        [MaxLength(50)]
        public string? index_id { get; set; }
        [Column("index_value")]
        [MaxLength(150)]
        public string? index_value { get; set; }
        [Column("created_at")]
        public DateTime created_at { get; set; }

        [Column("created_by")]
        [MaxLength(20)]
        public string? created_by { get; set; }

        [Column("updated_at")]
        public DateTime? updated_at { get; set; }

        [Column("last_updated_at")]
        public DateTime? last_updated_at { get; set; }

        [Column("updated_by")]
        [MaxLength(20)]
        public string? updated_by { get; set; }
    }
}
