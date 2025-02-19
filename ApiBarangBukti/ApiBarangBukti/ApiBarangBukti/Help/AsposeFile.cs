namespace ApiBarangBukti.Help
{
    public static class AsposeFile
    {
        public static byte[] ConvertWordToPDF(byte[] byteArray, string fileName)
        {
            //Aspose.Words.License license = new Aspose.Words.License();
            //license.SetLicense(@"Aspose.Words.lic");

            byte[] byteArrayPDF;

            Stream stream = new MemoryStream(byteArray);
            Aspose.Words.Document doc = new Aspose.Words.Document(stream);
            doc.Save(fileName, Aspose.Words.SaveFormat.Pdf);
            byteArrayPDF = File.ReadAllBytes(fileName);
            return byteArrayPDF;

        }

        //public static byte[] ConvertImageToPDF(byte[] byteArray, string fileName)
        //{
        //    //Aspose.Pdf.License license = new Aspose.Pdf.License();
        //    //license.SetLicense("Aspose.Pdf.lic");

        //    byte[] byteArrayPDF;

        //    Aspose.Pdf.Document pdf1 = new Aspose.Pdf.Document();
        //    Aspose.Pdf.Page sec = pdf1.Pages.Add();
        //    Stream stream = new MemoryStream(byteArray);
        //    Aspose.Pdf.Image imageht = new Aspose.Pdf.Image();
        //    imageht.ImageStream = stream;
        //    sec.Paragraphs.Add(imageht);

        //    pdf1.Save(fileName, Aspose.Pdf.SaveFormat.Pdf);
        //    byteArrayPDF = File.ReadAllBytes(fileName);
        //    return byteArrayPDF;

        //}

    }
}
