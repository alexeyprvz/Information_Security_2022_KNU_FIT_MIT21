using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PW_7_8._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string original;
            byte[] plainText;
            string pathToXMLFolder = "..\\..\\..\\.\\xml\\";
            string pathToXMLFile;
            string pathToFolderEncMsgs = "..\\..\\..\\.\\xml\\enc_msgs\\";
            string pathToEncMsgFile;
            string surname;
            string txtFileName;
            string pkey_ext = ".xml";
            string encMsg_ext = ".txt";
            char sw;

            Console.Write("g - generate new keys\n" +
                "r - remove keys\n" +
                "e - encypt data\n" +
                "d - decrypt data\n" +
                " -> ");
            sw = Convert.ToChar(Console.ReadLine());

            switch (sw)
            {
                case 'g':
                    Console.Write("Write your surname: ");
                    surname = Console.ReadLine();
                    pathToXMLFile = pathToXMLFolder + surname + pkey_ext;
                    ASYM_ENC.GenerateOwnKeys(pathToXMLFile);
                    Console.WriteLine("DONE");
                    break;

                case 'r':
                    try
                    {
                        Console.Write("Write your surname: ");
                        surname = Console.ReadLine();
                        pathToXMLFile = pathToXMLFolder + surname + pkey_ext;
                        ASYM_ENC.DeleteKeys(pathToXMLFile);

                        var files = Directory.GetFiles(pathToFolderEncMsgs);
                        for (int i = 0; i < files.Length; i++)
                        {
                            File.Delete(files[i]);
                        }

                        Console.WriteLine("DONE");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    break;

                case 'e':
                    try
                    {
                        var files = Directory.GetFiles(pathToXMLFolder);
                        if (files.Length != 0)
                        {
                            Console.Write("Enter data to encrypt: ");
                            original = Console.ReadLine();

                            Console.Write("Write file name to encrypt (without extantion): ");
                            txtFileName = Console.ReadLine();
                            pathToEncMsgFile = pathToFolderEncMsgs + txtFileName + encMsg_ext;

                            Console.WriteLine("Public keys:");
                            for (int i = 0; i < files.Length; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + files[i]);
                            }

                            Console.Write("\nChoose public key to encrypt (enter number): ");
                            int num = Convert.ToInt32(Console.ReadLine());
                            pathToXMLFile = files[num - 1];

                            ASYM_ENC.EncryptData(pathToXMLFile, Encoding.UTF8.GetBytes(original), pathToEncMsgFile);

                            Console.WriteLine("DONE");
                        }
                        else
                        {
                            Console.WriteLine("NO PUBLIC KEYS TO ENCRYPT");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    break;

                case 'd':
                    try
                    {
                        var files = Directory.GetFiles(pathToFolderEncMsgs);
                        if (files.Length != 0)
                        {
                            Console.WriteLine("Encrypted messages:");
                            for (int i = 0; i < files.Length; i++)
                            {
                                Console.WriteLine((i + 1) + ". " + files[i]);
                            }

                            Console.Write("\nChoose message to dencrypt (enter number): ");
                            int num = Convert.ToInt32(Console.ReadLine());
                            pathToEncMsgFile = files[num - 1];

                            plainText = ASYM_ENC.DecryptData(pathToEncMsgFile);

                            Console.WriteLine("Original text: " + Encoding.Default.GetString(plainText));
                        }
                        else
                        {
                            Console.WriteLine("NO FILES TO DECRYPT");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    break;
                default:
                    break;
            }
        }
    }
    public class ASYM_ENC
    {
        const string CspContainerName = "MyContainer";
        public static void GenerateOwnKeys(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider",
            }; 
            using (var rsa = new RSACryptoServiceProvider(2048, cspParameters))
            {
                rsa.PersistKeyInCsp = true; 
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
            }
        }
        public static void DeleteKeys(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = false
            };
            File.Delete(publicKeyPath);
            rsa.Clear();
        }
        public static void EncryptData(string publicKeyPath, byte[] dataToEncrypt, string chipherTextPath)
        {
            byte[] chipherBytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath)); 
                chipherBytes = rsa.Encrypt(dataToEncrypt, true);
            }
            File.WriteAllBytes(chipherTextPath, chipherBytes);
        }
        public static byte[] DecryptData(string chipherTextPath)
        {
            byte[] chipherBytes = File.ReadAllBytes(chipherTextPath); 
            byte[] plainTextBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            }; 
            using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
            {
                rsa.PersistKeyInCsp = true; 
                plainTextBytes = rsa.Decrypt(chipherBytes, true);
            } 
            return plainTextBytes; 
        }
    }
}
