namespace WebBarangBukti.Models
{
    public class HdTransaksi 
    {
        public int Id { get; set; }

        public string? IdTransaksi { get; set; }

        public string? NamaTransaksi { get; set; }

        public int? JenisTransaksi { get; set; }

        public string? JudulTransaksi { get; set; }

        public string? NoPerkara { get; set; }

        public string? Pic { get; set; }

        public DateTime? TanggalTransaksi { get; set; }

        public string? File { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string? CreateBy { get; set; }

        public string? FileName { get; set; }

        public string? ContentType { get; set; }

        public string? Extension { get; set; }

        public string? Base64File { get; set; }

        public int? FileSize { get; set; }

        public IFormFile? files { get; set; }
    }
}
