namespace WebBarangBukti.Models
{
    public class DtTransaksi
    {
        public int Id { get; set; }

        public string? IdTransaksi { get; set; }

        public string? NoPerkara { get; set; }

        public string? IdBarangBukti { get; set; }

        public string? PemenangLelangInstansi { get; set; }

        public decimal? Harga { get; set; }

        public DateTime? TanggalTerjual { get; set; }

        public DateTime? TanggalPenyerahan { get; set; }

        public string? File { get; set; }

        public int? JenisTransaksi { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public string? CreateBy { get; set; }

        public string? FileName { get; set; }

        public string? ContentType { get; set; }

        public string? Extension { get; set; }

        public int? FileSize { get; set; }

        public string? Base64File { get; set; }

        public string? FilePath { get; set; }

        public IFormFile? files { get; set; }
    }
}
