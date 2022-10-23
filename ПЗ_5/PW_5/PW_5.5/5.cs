using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;

namespace PW_5._5
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Choose command: " +
                "\nl - login" +
                "\nr - register" +
                "\ne - exit" +
                "\n->");
            char c = Convert.ToChar(Console.ReadLine());

            string path = "..\\..\\..\\.\\users\\";
            string ext_txt = ".txt";
            string tofilepassword = "_p";
            string tofilesalt = "_s";


            string login;
            string password;
            string filepath_salt;
            string filepath_password;
            string hashpassword;
            byte[] bsalt = new byte[32];
            string filedir;
            var sw = new Stopwatch();

            switch (c)
            {
                case 'l':
                    Console.Write("Enter login: ");
                    login = Console.ReadLine();

                    filedir = path + login + "\\";
                    filepath_password = filedir + login + tofilepassword + ext_txt;
                    filepath_salt = filedir + login + tofilesalt + ext_txt;

                    Console.Write("Enter password: ");
                    password = Console.ReadLine();

                    try
                    {
                        sw.Start();
                        using (StreamReader read = File.OpenText(filepath_password))
                        {
                            hashpassword = read.ReadLine();
                        }

                        using (var read = new FileStream(filepath_salt, FileMode.Open))
                        {
                            read.Read(bsalt, 0, bsalt.Length);
                        }
                        password = Convert.ToBase64String(PBKDF2.HashPassword(password, bsalt, 200000, HashAlgorithmName.SHA512));
                        sw.Stop();
                        if (password == hashpassword)
                        {
                            Console.WriteLine("\nLOGIN SUCCESSFUL\n" +
                                "Authorization time: " + sw.ElapsedMilliseconds + "ms");
                        }
                        else
                        {
                            Console.WriteLine("\nLOGIN FAILED");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\nUSER DOES NOT EXIST");
                    }
                    break;

                case 'r':
                    Console.Write("Enter login: ");
                    login = Console.ReadLine();
                    Console.Write("Enter password: ");
                    password = Console.ReadLine();

                    sw.Start();
                    bsalt = PBKDF2.GenerateSalt();
                    password = Convert.ToBase64String(PBKDF2.HashPassword(password, bsalt, 200000, HashAlgorithmName.SHA512));
                    filedir = path + login + "\\";
                    Directory.CreateDirectory(filedir);
                    filepath_password = filedir + login + tofilepassword + ext_txt;
                    filepath_salt = filedir + login + tofilesalt + ext_txt;

                    try
                    {
                        
                        using (var stream = new FileStream(filepath_salt, FileMode.Create))
                        {
                            stream.Write(bsalt, 0, bsalt.Length);
                        };

                        using (StreamWriter write = new StreamWriter(filepath_password))
                        {
                            write.WriteLine(password);
                        };
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    sw.Stop();
                    Console.WriteLine("\nDONE\n" +
                        "Registration time: " + sw.ElapsedMilliseconds + "ms");
                    break;

                case 'e':
                default:
                    Console.WriteLine("\nExit");
                    break;
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
        public static byte[] HashPassword(string toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName alg)
        {

            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, alg))
            {
                return rfc2898.GetBytes(20);
            }
        }
    }
}
