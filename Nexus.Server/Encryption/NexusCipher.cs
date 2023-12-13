using Nexus.Server.Entity;
using Nexus.Server.Models;
using System.Security.Cryptography;
using System.Text;

namespace Nexus.Server.Encryption
{
    public class NexusCipher
    {
        public static string ToSHA256(string password)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] hashStr = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                string temp = "";

                foreach (byte b in hashStr)
                    temp += $"{b:X2}";

                return temp;
            }
        }

        public static void Encrypt(Message message)
        {
            string EncryptionKey = "abc123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(message.TextContent);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    message.TextContent = Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(Message_Model message)
        {
            string message_text = message.TextContent;
            string EncryptionKey = "abc123";
            message_text = message_text.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(message_text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    message_text = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return message_text;
        }
    }
}
