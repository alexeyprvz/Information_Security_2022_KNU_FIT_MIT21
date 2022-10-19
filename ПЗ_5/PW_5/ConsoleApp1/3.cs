using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PW_5._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter password: ");
            string password = Console.ReadLine(); 
            byte[] salt = SaltedHash.GenerateSalt();
            Console.WriteLine("\nSalt: " + Convert.ToBase64String(salt));
            var hashedPassword = SaltedHash.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password), salt);
            Console.WriteLine("Hashed Password: " + Convert.ToBase64String(hashedPassword));
        }
    }
    public class SaltedHash
    {
        public static byte[] GenerateSalt()
        {
            using (var rndnumgen = new RNGCryptoServiceProvider())
            {
                var rndnum = new byte[32]; 
                rndnumgen.GetBytes(rndnum);
                return rndnum;
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        public static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt) 
        { 
            using (var sha256 = SHA256.Create()) 
            { 
                return sha256.ComputeHash(Combine(toBeHashed, salt)); 
            } 
        }
    }
}