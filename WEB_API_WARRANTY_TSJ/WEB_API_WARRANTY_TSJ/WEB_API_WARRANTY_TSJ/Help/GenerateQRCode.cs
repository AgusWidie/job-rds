using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ZXing;
using ZXing.Windows.Compatibility;


namespace WEB_API_WARRANTY_TSJ.Help
{
    public class GenerateQRCode
    {
        public static Bitmap ProcessQR(string barcode)
        {
            var barcodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 1300,
                    Height = 1300
                }
            };

            // Generate the QR code image from the input text
            var pixelData = barcodeWriter.Write(barcode);
#pragma warning disable CA1416 // Validate platform compatibility
            Bitmap bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppArgb);
#pragma warning restore CA1416 // Validate platform compatibility
            MemoryStream ms = new MemoryStream(pixelData.Pixels);
#pragma warning disable CA1416 // Validate platform compatibility
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
#pragma warning restore CA1416 // Validate platform compatibility
            try
            {
                Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
            return new Bitmap(bitmap);
        }

        public static string Base64FromBitmap(Bitmap data)
        {
            // Load the bitmap image
            Bitmap bitmap = new Bitmap(data);

            // Convert the bitmap to a byte array
            byte[] bitmapBytes;
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                bitmapBytes = stream.ToArray();
            }

            // Convert the byte array to a Base64 string
            string base64String = Convert.ToBase64String(bitmapBytes);
            return base64String;
        }

        public static Bitmap GenerateBarcode(string textBarcode)
        {
            Bitmap bitm = new Bitmap(textBarcode.Length * 40, 160);
            using (Graphics graphic = Graphics.FromImage(bitm))
            {
                Font newfont = new Font("IDAutomationHC39M", 20);
                PointF point = new PointF(2f, 2f);
                SolidBrush black = new SolidBrush(Color.Black);
                SolidBrush white = new SolidBrush(Color.White);
                graphic.FillRectangle(white, 0, 0, bitm.Width, bitm.Height);
                graphic.DrawString("*" + textBarcode + "*", newfont, black, point);
            }

            return bitm;
        }
    }
}
