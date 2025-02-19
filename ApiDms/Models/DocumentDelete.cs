using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDms.Models
{
    [Table("document_delete")]
    public class DocumentDelete
    {
        [Key]
        [Required]
        [Column("document_delete_id")]
        [MaxLength(50)]
        public string? document_delete_id { get; set; }

        [Column("user_id")]
        [MaxLength(50)]
        public string? user_id { get; set; }

        [Column("document_id")]
        [MaxLength(50)]
        public string? document_id { get; set; }

        [Column("status")]
        public int? status { get; set; }

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
    }
}
