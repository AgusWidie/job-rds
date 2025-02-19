namespace ApiBarangBukti.Help
{
    public static class GenNumber
    {
        public static string LastDetailIdBarangBukti(string? IdDtBarangBukti)
        {
            string res_id_detail_barang_bukti = "";
            int no_urut = 0;

            no_urut = Convert.ToInt32(IdDtBarangBukti.Substring(3, 3)) + 1;

            if (no_urut < 10) {
                res_id_detail_barang_bukti = "BB-" + "00" + no_urut.ToString();
            }

            else if (no_urut < 100) {
                res_id_detail_barang_bukti = "BB-" + "0" + no_urut.ToString();
            }

            else if (no_urut < 1000) {
                res_id_detail_barang_bukti = "BB-" + no_urut.ToString();
            }

            else {
                res_id_detail_barang_bukti = "BB-" + no_urut.ToString();
            }

            return res_id_detail_barang_bukti;
        }

        public static string FirstDetailIdBarangBukti()
        {
            return "BB-001";
        }
    }
}
