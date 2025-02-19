using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDms.Models
{
    [Table("directories")]
    public class Directory
    {
        [Key]
        [Required]
        [Column("id")]
        public int id { get; set; }

        [Column("directory_id")]
        [MaxLength(50)]
        public string? directory_id { get; set; }
        [Column("directory_name")]
        public string? directory_name { get; set; }
        [Column("disk")]
        public string? disk { get; set; }
        [Column("path_name")]
        public string? path_name { get; set; }
        [Column("parent_id")]
        public int parent_id { get; set; }
        [Column("collection_id")]
        [MaxLength(50)]
        public string? collection_id { get; set; }
        [Column("owner_id")]
        [MaxLength(10)]
        public string? owner_id { get; set; }

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

    }
}
