using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace OnlineMarketPlace.ClassLibraries
{
    public class CustomizedCryptography
    {
        private static byte[] _byteRGbKey = { 1, 2, 3, 0, 0, 5, 2, 6, 8, 3, 5, 4, 3, 2, 1, 8 };
        private static byte[] _byteRGBIV = { 230, 50, 1, 5, 23, 2, 70, 17, 18, 19, 20, 141, 10, 12, 45, 16 };
        public static string Encrypt(string text)
        {
            try
            {
                if (text != null)
                {
                    var aes = Aes.Create();
                    var key = aes.CreateEncryptor(_byteRGbKey, _byteRGBIV);
                    MemoryStream memory = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memory, key, CryptoStreamMode.Write);
                    StreamWriter writer = new StreamWriter(cryptoStream);
                    writer.Write(text);
                    writer.Close();

                    return Convert.ToBase64String(memory.ToArray());
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex) { return null; }
        }

        public static string Decrypt(string cypherText)
        {
            try
            {
                if (cypherText != null)
                {
                    var aes = Aes.Create();
                    var key = aes.CreateDecryptor(_byteRGbKey, _byteRGBIV);
                    MemoryStream memory = new MemoryStream(Convert.FromBase64String(cypherText));
                    CryptoStream cryptoStream = new CryptoStream(memory, key, CryptoStreamMode.Read);
                    StreamReader reader = new StreamReader(cryptoStream);
                    string text = reader.ReadToEnd();
                    reader.Close();

                    return text;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex) { return null; }
        }
    }
}
