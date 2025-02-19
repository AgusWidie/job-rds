using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Web;

namespace WEB_DASHBOARD_INV.Helper
{
    public class QRCode
    {
        public static Bitmap GenerateQRCode(string barcode)
        {
            var barcodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 1200,
                    Height = 1200
                }
            };

            // Generate the QR code image from the input text
            var pixelData = barcodeWriter.Write(barcode);
            Bitmap bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppArgb);
            MemoryStream ms = new MemoryStream(pixelData.Pixels);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
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
    }
}