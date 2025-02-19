namespace ApiBarangBukti.Help
{
    public static class GetNewID
    {
        public static string GenNewID()
        {
            var ticks = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var myUniqueFileName = ans.ToString("x").ToLower();
            return myUniqueFileName;
        }
    }
}
