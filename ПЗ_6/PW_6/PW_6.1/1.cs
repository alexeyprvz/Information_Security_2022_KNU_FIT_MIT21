using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PW_6._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string message;
            byte[] b_message;
            byte[] enc_message;
            byte[] dec_message;
            byte[] key;
            byte[] iv;

            int sw;

            Console.Write("Write message to encrypt: ");
            message = Console.ReadLine();

            Console.Write("Choose algoritm:\n" +
                "1 - DES\n" +
                "2 - TripleDES\n" +
                "3 - AES\n" +
                " -> ");
            sw = Convert.ToInt32(Console.ReadLine());

            b_message = Encoding.UTF8.GetBytes(message);

            switch (sw)
            {
                case '1':
                    key = ENC_DEC.rndNum(8);
                    iv = ENC_DEC.rndNum(8);

                    enc_message = ENC_DEC.DES_Encrypt(b_message, key, iv);
                    Console.WriteLine("DES Encrypt: " + Convert.ToBase64String(enc_message));

                    dec_message = ENC_DEC.DES_Decrypt(enc_message, key, iv);
                    Console.WriteLine("DES Decrypted: " + Encoding.UTF8.GetString(dec_message));
                    break;

                case '2':
                    key = ENC_DEC.rndNum(16);
                    iv = ENC_DEC.rndNum(8);

                    enc_message = ENC_DEC.TDES_Encrypt(b_message, key, iv);
                    Console.WriteLine("TripleDES Encrypt: " + Convert.ToBase64String(enc_message));

                    dec_message = ENC_DEC.TDES_Decrypt(enc_message, key, iv);
                    Console.WriteLine("TripleDES Decrypted: " + Encoding.UTF8.GetString(dec_message));
                    break;

                case '3':
                default:
                    key = ENC_DEC.rndNum(32);
                    iv = ENC_DEC.rndNum(16);

                    enc_message = ENC_DEC.AES_Encrypt(b_message, key, iv);
                    Console.WriteLine("AES Encrypt: " + Convert.ToBase64String(enc_message));

                    dec_message = ENC_DEC.AES_Decrypt(enc_message, key, iv);
                    Console.WriteLine("AES Decrypted: " + Encoding.UTF8.GetString(dec_message));
                    break;
            }
        }
    }

    public class ENC_DEC
    {
        public static byte[] rndNum(int length)
        {
            using (var rndNumGen = new RNGCryptoServiceProvider())
            {
                var num = new byte[length]; 
                rndNumGen.GetBytes(num); 
                return num;
            }
        }
        public static byte[] AES_Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC; 
                aes.Padding = PaddingMode.PKCS7; 
                aes.Key = key; 
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock(); 
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] AES_Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key; 
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock(); 
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] DES_Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new DESCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] DES_Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var aes = new DESCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] TDES_Encrypt(byte[] dataToEncrypt, byte[] key, byte[] iv)
        {
            using (var aes = new TripleDESCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public static byte[] TDES_Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var aes = new TripleDESCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
    }

}
