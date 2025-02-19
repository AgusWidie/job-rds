using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ApiDms.Help
{
    public static class Crypto
    {
        public static string ReadAsString(this IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            return result.ToString();
        }

        //public static byte[] EncryptSha512(byte[] bytes)
        //{
        //    SHA512CryptoServiceProvider sha512 = new SHA512CryptoServiceProvider();
        //    byte[] hash = sha512.ComputeHash(bytes);
        //    //byte[] hash = sha256.TransformFinalBlock(bytes, 0, bytes.Length);
        //    return hash;
        //}

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


        public static string sha512(byte[] bytes)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

     
        public static byte[] CreateSHAHash(string file_string)
        {
            string file_key = "lP9fH0N9CYgjKy3AexqOLw==";
            SHA512Managed sha512 = new SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(file_string, file_key)));
            sha512.Clear();
            return EncryptedSHA512;
        }

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

        //public static void EncryptFile(string inFile, string outFile,
        //            string password, CryptoProgressCallBack callback)
        //{
        //    using (FileStream fin = File.OpenRead(inFile),
        //          fout = File.OpenWrite(outFile))
        //    {
        //        long lSize = fin.Length; // the size of the input file for storing

        //        int size = (int)lSize;  // the size of the input file for progress

        //        byte[] bytes = new byte; // the buffer

        //        int read = -1; // the amount of bytes read from the input file

        //        int value = 0; // the amount overall read from the input file for progress


        //        // generate IV and Salt

        //        byte[] IV = GenerateRandomBytes(16);
        //        byte[] salt = GenerateRandomBytes(16);

        //        // create the crypting object

        //        SymmetricAlgorithm sma = CryptoHelp.CreateRijndael(password, salt);
        //        sma.IV = IV;

        //        // write the IV and salt to the beginning of the file

        //        fout.Write(IV, 0, IV.Length);
        //        fout.Write(salt, 0, salt.Length);

        //        // create the hashing and crypto streams

        //        HashAlgorithm hasher = SHA256.Create();
        //        using (CryptoStream cout = new CryptoStream(fout, sma.CreateEncryptor(), CryptoStreamMode.Write), chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
        //        {
        //            // write the size of the file to the output file

        //            BinaryWriter bw = new BinaryWriter(cout);
        //            bw.Write(lSize);

        //            // write the file cryptor tag to the file

        //            bw.Write(FC_TAG);

        //            // read and the write the bytes to the crypto stream 

        //            // in BUFFER_SIZEd chunks

        //            while ((read = fin.Read(bytes, 0, bytes.Length)) != 0)
        //            {
        //                cout.Write(bytes, 0, read);
        //                chash.Write(bytes, 0, read);
        //                value += read;
        //                callback(0, size, value);
        //            }
        //            // flush and close the hashing object

        //            chash.Flush();
        //            chash.Close();

        //            // read the hash

        //            byte[] hash = hasher.Hash;

        //            // write the hash to the end of the file

        //            cout.Write(hash, 0, hash.Length);

        //            // flush and close the cryptostream

        //            cout.Flush();
        //            cout.Close();
        //        }
        //    }
        //}
    }
}
