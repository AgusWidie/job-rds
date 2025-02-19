﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDms.Models
{
    [Table("collection_document_types")]
    public class CollectionDocumentType
    {
        [Key]
        [Required]
        [Column("id")]
        public int id { get; set; }
        [Column("collection_id")]
        [MaxLength(50)]
        public string? collection_id { get; set; }
        [Column("document_type_id")]
        [MaxLength(50)]
        public string? document_type_id { get; set; }

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