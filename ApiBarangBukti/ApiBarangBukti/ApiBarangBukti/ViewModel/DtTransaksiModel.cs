namespace ApiBarangBukti.ViewModel
{
    public class DtTransaksiModel
    {
        public int Id { get; set; }

        public string? IdTransaksi { get; set; }

        public string? IdBarangBukti { get; set; }

        public string? NamaBarangBukti { get; set; }

        public int? JenisTransaksi { get; set; }

        public string? PemenangLelangInstansi { get; set; }

        public decimal? Harga { get; set; }

        public string? TanggalTerjual { get; set; }

        public string? TanggalPenyerahan { get; set; }

        public string? File { get; set; }
    }
}
