using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PW._7_8._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Write text to encrypt: ");
            string original = Console.ReadLine();

            ASYM_ENC.AssignNewKey();

            var encrypted = ASYM_ENC.EncryptData(Encoding.UTF8.GetBytes(original));

            var decrypted = ASYM_ENC.DecryptData(encrypted);

            Console.WriteLine("\nEncrypted text: " + Convert.ToBase64String(encrypted));
            Console.WriteLine("\nDecrypted text: " + Encoding.Default.GetString(decrypted));
        }
    }

    public class ASYM_ENC
    {
        private static RSAParameters _publicKey, _privateKey;

        public static void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }

        public static byte[] EncryptData(byte[] dataToEncrypt) { 
            byte[] cipherbytes; 
            using (var rsa = new RSACryptoServiceProvider()) 
            { 
                rsa.PersistKeyInCsp = false; 
                rsa.ImportParameters(_publicKey); 
                cipherbytes = rsa.Encrypt(dataToEncrypt, true); 
            } 
            return cipherbytes; 
        }
        public static byte[] DecryptData(byte[] dataToEncrypt) 
        { 
            byte[] plain; 
            using (var rsa = new RSACryptoServiceProvider()) 
            { 
                rsa.PersistKeyInCsp = false; 
                rsa.ImportParameters(_privateKey); 
                plain = rsa.Decrypt(dataToEncrypt, true); 
            } 
            return plain; 
        }
    }
}
