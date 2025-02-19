namespace WebBarangBukti.Models
{
    public class DtBarangBukti
    {
        public int Id { get; set; }

        public string? IdDtBarangBukti { get; set; }

        public string? NamaBarangBukti { get; set; }

        public string? Identitas { get; set; }

        public string? Kondisi { get; set; }

        public int? Jumlah { get; set; }

        public string? StatusEksekusi { get; set; }

        public string? StatusAkhir { get; set; }

        public string? File { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? DeleteAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public string? CreateBy { get; set; }

        public string? IdHdBarangBukti { get; set; }

        public string? FileName { get; set; }

        public string? ContentType { get; set; }

        public string? Extension { get; set; }

        public int? FileSize { get; set; }

        public string? Base64File { get; set; }

        public string? FilePath { get; set; }

        public IFormFile? files { get; set; }
    }
}
