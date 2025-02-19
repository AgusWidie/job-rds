
using System.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using PdfSharp.Pdf;
using System.IO.Packaging;
using System.Xml.Linq;
using System.Xml;
namespace ApiDms.Help
{
    public static class ConvertFile
    {


        public static byte[] ConvertDocxToPdf(byte[] docxByteArray)
        {
            // Create an HTML builder
            StringBuilder htmlContent = new StringBuilder();
            using (MemoryStream docxStream = new MemoryStream(docxByteArray))
            {
                // Open the DOCX file as a ZIP archive (it is a zipped XML)
                using (Package package = Package.Open(docxStream, FileMode.Open, FileAccess.Read))
                {
                    // Get the main document part (document.xml)
                    var documentPart = package.GetPart(new Uri("/word/document.xml", UriKind.Relative));

                    // Load the XML content of the document
                    XDocument doc = XDocument.Load(XmlReader.Create(documentPart.GetStream()));
                    htmlContent.Append("<html><body>");

                    // Parse paragraphs in the DOCX XML
                    foreach (var paragraph in doc.Descendants("{http://schemas.openxmlformats.org/wordprocessingml/2006/main}p"))
                    {
                        htmlContent.Append("<p>");
                        foreach (var run in paragraph.Descendants("{http://schemas.openxmlformats.org/wordprocessingml/2006/main}r"))
                        {
                            foreach (var text in run.Descendants("{http://schemas.openxmlformats.org/wordprocessingml/2006/main}t"))
                            {
                                htmlContent.Append(text.Value);
                            }
                        }
                        htmlContent.Append("</p>");
                    }

                    htmlContent.Append("</body></html>");

                }
            }

            // Generate PDF document from HTML content
            PdfDocument pdf = PdfGenerator.GeneratePdf(htmlContent.ToString(), PdfSharp.PageSize.A4);

            // Save the PDF to a memory stream and return the byte array
            using (MemoryStream ms = new MemoryStream())
            {
                pdf.Save(ms, false);
                return ms.ToArray();
            }

        }


        public static byte[] ConvertImageToPDF(byte[] byteArrayFile, string fileName)
        {

            MemoryStream streamImage = new MemoryStream(byteArrayFile);

            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 40, 15, 15);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();

                var file = iTextSharp.text.Image.GetInstance(streamImage);
                file.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                document.Add(file);

                document.Close();
                document.Dispose();
                return ms.ToArray();
            }


        }

       
    }
}
