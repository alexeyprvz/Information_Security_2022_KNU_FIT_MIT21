using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace PW_6._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string password;
            string message;
            char sw;

            string path = "..\\..\\..\\.\\files\\";
            string path_key = "..\\..\\..\\.\\files\\key.txt";
            string path_iv = "..\\..\\..\\.\\files\\iv.txt";
            string path_enc_message = "..\\..\\..\\.\\files\\enc_message.txt";

            byte[] enc_message = new byte[16];
            byte[] dec_message;
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            int itr = 200000;

            Console.Write("c - create new key and vector\n" +
                "s - show key and vector\n" +
                "e - encrypt message\n" +
                "d - decrypt message\n" +
                " -> ");
            sw = Convert.ToChar(Console.ReadLine());

            switch (sw)
            {
                case 'c':
                    Console.Write("Enter password: ");
                    password = Console.ReadLine();

                    key = ENC_DEC.Genereate_Key_Vector(password, itr, 'k');
                    iv = ENC_DEC.Genereate_Key_Vector(password, itr, 'v');

                    using (var stream = new FileStream(path_key, FileMode.Create))
                    {
                        stream.Write(key, 0, key.Length);
                    };

                    using (var stream = new FileStream(path_iv, FileMode.Create))
                    {
                        stream.Write(iv, 0, iv.Length);
                    };

                    Console.WriteLine("DONE");
                    break;

                case 'e':
                    Console.Write("Enter message: ");
                    message = Console.ReadLine();

                    using (var read = new FileStream(path_key, FileMode.Open))
                    {
                        read.Read(key, 0, key.Length);
                    }

                    using (var read = new FileStream(path_iv, FileMode.Open))
                    {
                        read.Read(iv, 0, iv.Length);
                    }

                    enc_message = ENC_DEC.AES_Encrypt(Encoding.UTF8.GetBytes(message), key, iv);

                    using (var stream = new FileStream(path_enc_message, FileMode.Create))
                    {
                        stream.Write(enc_message, 0, enc_message.Length);
                    };

                    Console.WriteLine("AES Encrypt: " + Convert.ToBase64String(enc_message));
                    break;

                case 'd':

                    using (var read = new FileStream(path_key, FileMode.Open))
                    {
                        read.Read(key, 0, key.Length);
                    }

                    using (var read = new FileStream(path_iv, FileMode.Open))
                    {
                        read.Read(iv, 0, iv.Length);
                    }

                    using (var read = new FileStream(path_enc_message, FileMode.Open))
                    {
                        read.Read(enc_message, 0, enc_message.Length);
                    }
                    Console.WriteLine("AES Encrypt: " + Convert.ToBase64String(enc_message));

                    dec_message = ENC_DEC.AES_Decrypt(enc_message, key, iv);
                    Console.WriteLine("AES Decrypt: " + Encoding.UTF8.GetString(dec_message));

                    break;

                case 's':
                    using (var read = new FileStream(path_key, FileMode.Open))
                    {
                        read.Read(key, 0, key.Length);
                    }

                    using (var read = new FileStream(path_iv, FileMode.Open))
                    {
                        read.Read(iv, 0, iv.Length);
                    }

                    Console.WriteLine("Key:" + Encoding.UTF8.GetString(key) + "\n" +
                        "Vector:" + Encoding.UTF8.GetString(iv));

                    break;

                default:
                    break;
            }

            

            


        }
    }

    public class ENC_DEC
    {
        public static byte[] Genereate_Key_Vector(string toBeHashed, int itr, char c)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, 32, itr, HashAlgorithmName.SHA512))
            {
                if (c == 'k')
                    return rfc2898.GetBytes(32); //повертає ключ
                if (c == 'v')
                    return rfc2898.GetBytes(16); //повертає вектор ініціалізації
                return rfc2898.GetBytes(32);
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
    }
}
