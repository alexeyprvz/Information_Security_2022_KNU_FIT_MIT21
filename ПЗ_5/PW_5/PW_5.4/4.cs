using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace PW_5._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Console.Write("\n1 - MD5" +
                "\n2 - SHA1" +
                "\n3 - SHA256" +
                "\n4 - SHA284" +
                "\n5 - SHA512" +
                "\n -> ");
            int alg_chs = Convert.ToInt32(Console.ReadLine());

            HashAlgorithmName alg = HashAlgorithmName.SHA1;
            if (alg_chs == 1)
                alg = HashAlgorithmName.MD5;
            if (alg_chs == 2)
                alg = HashAlgorithmName.SHA1;
            if (alg_chs == 3)
                alg = HashAlgorithmName.SHA256;
            if (alg_chs == 4)
                alg = HashAlgorithmName.SHA384;
            if (alg_chs == 5)
                alg = HashAlgorithmName.SHA512;

            int iter_step = 50000;
            int cnt = 0;
            byte[] salt;
            int firstnum = iter_step * 20;

            while (cnt != 10)
            {
                var sw = new Stopwatch();
                salt = PBKDF2.GenerateSalt();
                sw.Start();
                string hashedPassword = Convert.ToBase64String(PBKDF2.HashPassword(Encoding.UTF8.GetBytes(password), salt,firstnum + (cnt * iter_step), alg));
                sw.Stop();

                Console.WriteLine("Hashed password: " + hashedPassword + "    Salt: " + Convert.ToBase64String(salt) +
                    "     Iterations: " + (firstnum + (cnt * iter_step)) + "    Time: " + sw.ElapsedMilliseconds);
                cnt++;
                sw.Reset();
            }
            
        }

    }

    public class PBKDF2
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
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName alg) {

            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, alg)) { 
                return rfc2898.GetBytes(20); 
            } 
        }
    }
}
