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
    }
}
