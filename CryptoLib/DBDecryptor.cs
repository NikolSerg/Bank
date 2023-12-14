
using CryptoLib.Properties;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Forms;

namespace Bank__v1
{
    public static class DBDecryptor
    {
        static string dataBase;
        static AesCng aes;
        static RSACng rsa;
        static DBDecryptor()
        {
            rsa = new RSACng(CngKey.Create(CngAlgorithm.Rsa));
            aes = new AesCng();
            rsa.FromXmlString(Resources.privateKey);
            aes.Key = rsa.Decrypt(Resources.SimmKey, RSAEncryptionPadding.OaepSHA256);
            aes.IV = rsa.Decrypt(Resources.SimmIV, RSAEncryptionPadding.OaepSHA256);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
        }
        public static string GetDataBase(string path)
        {
            ICryptoTransform dec = aes.CreateDecryptor(aes.Key, aes.IV);
            try
            {
                using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(path)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, dec, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            dataBase = sr.ReadToEnd();
                        }
                    }
                }
                return dataBase;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return String.Empty; }
        }

        public static void SaveDataBase(string db, string path)
        {
            ICryptoTransform dec = aes.CreateEncryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, dec, CryptoStreamMode.Write))
                {
                    using (StreamWriter sr = new StreamWriter(cs))
                    {
                        sr.Write(db);
                    }
                    byte[] bytes = ms.ToArray();
                    File.WriteAllBytes(path, bytes);
                }
            }
        }

    }
}
