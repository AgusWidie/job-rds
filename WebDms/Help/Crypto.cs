using System.Security.Cryptography;
using System.Text;

namespace WebDms.Help
{
    public static class Crypto
    {
        public static string key { get; set; } = "";
        public static string EncryptData(byte[] encryptionData)
        {

            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        //byte[] textBytes = UTF8Encoding.UTF8.GetBytes(encryptionText);
                        byte[] bytes = transform.TransformFinalBlock(encryptionData, 0, encryptionData.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                        //return bytes;
                    }
                }
            }
        }

        public static byte[] DecryptData(string input)
        {

            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(input);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        //return UTF8Encoding.UTF8.GetString(bytes);
                        return bytes;
                    }
                }
            }
        }

        public static string EncryptSha256(byte[] file_bytes)
        {
            Encoding enc = Encoding.UTF8;
            byte[] encryptByte;
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] byte_key = sha256.ComputeHash(enc.GetBytes("A6u55Widii33"));
            byte[] byte_IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = byte_key;
                aesAlg.IV = byte_IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                       new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(file_bytes, 0, file_bytes.Length);
                    }

                    encryptByte = memoryStream.ToArray();
                }

            }

            return Convert.ToBase64String(encryptByte);
        }

        public static byte[] DecryptSha256(string base64)
        {
            Encoding enc = Encoding.UTF8;
            byte[] decryptByte;
            byte[] file_bytes = Convert.FromBase64String(base64);
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] byte_key = sha256.ComputeHash(enc.GetBytes("A6u55Widii33"));
            byte[] byte_IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = byte_key;
                aesAlg.IV = byte_IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                       new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(file_bytes, 0, file_bytes.Length);
                    }

                    decryptByte = memoryStream.ToArray();
                }
            }

            return decryptByte;
        }

        public static byte[] EncryptFileSha256(byte[] file_bytes)
        {
            Encoding enc = Encoding.UTF8;
            byte[] encryptByte;
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] byte_key = sha256.ComputeHash(enc.GetBytes("A6u55Widii33"));
            byte[] byte_IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = byte_key;
                aesAlg.IV = byte_IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                encryptByte = encryptor.TransformFinalBlock(file_bytes, 0, file_bytes.Length);


                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                       new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(file_bytes, 0, file_bytes.Length);
                    }

                    encryptByte = memoryStream.ToArray();
                }
            }

            return encryptByte;
        }

        public static byte[] DecryptFileSha256(byte[] file_bytes)
        {
            Encoding enc = Encoding.UTF8;
            byte[] decryptByte;
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] byte_key = sha256.ComputeHash(enc.GetBytes("A6u55Widii33"));
            byte[] byte_IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = byte_key;
                aesAlg.IV = byte_IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream =
                       new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(file_bytes, 0, file_bytes.Length);
                    }

                    decryptByte = memoryStream.ToArray();
                }
            }

            return decryptByte;
        }
    }
}
