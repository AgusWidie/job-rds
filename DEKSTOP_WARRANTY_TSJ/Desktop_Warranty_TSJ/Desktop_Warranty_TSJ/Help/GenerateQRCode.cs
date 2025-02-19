using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Warranty_TSJ.Help
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
            //  Create a sample Linear barcode
            OnBarcode.Barcode.Linear linear = new OnBarcode.Barcode.Linear();
            linear.Type = OnBarcode.Barcode.BarcodeType.CODE128;
            linear.Data = textBarcode;
            linear.UOM = OnBarcode.Barcode.UnitOfMeasure.PIXEL;
            linear.Resolution = 96;
            linear.X = 2;
            linear.Y = 100;
            //  Render the barcode to a Bitmap object.
            System.Drawing.Bitmap bitmap = linear.drawBarcode();

            //  Convert Bitmap object to BitmapImage object.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return bitmap;
        }
    }
}
