using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace PW._9_10._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pathToPublicKey = "..\\..\\..\\.\\xml\\public.xml";

            Console.Write("Enter the message: ");
            var data = Encoding.UTF8.GetBytes(Console.ReadLine());

            byte[] hashedData;
            using (var sha512 = SHA512.Create())
            {
                hashedData = sha512.ComputeHash(data);
            }

            var digitalSignature = new DigitalSignature();
            digitalSignature.AssignNewKey(pathToPublicKey);
            var signature = digitalSignature.SignData(hashedData);
            var verified = digitalSignature.VerifySignature(pathToPublicKey, hashedData, signature);
            Console.WriteLine(" Digital Signature = " + Convert.ToBase64String(signature));
            Console.WriteLine(verified ? "The digital signature has been correctly verified." : "The digital signature has NOT been correctly verified.");
        }
    }

    public class DigitalSignature
    {
        private readonly static string CspContainerName = "RsaContainer";

        public void AssignNewKey(string publicKeyPath)
        {
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
            {
                rsa.PersistKeyInCsp = true;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
            }
        }

        public byte[] SignData(byte[] hashOfDataToSign)
        {
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
            };

            using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
            {
                rsa.PersistKeyInCsp = false;
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm(nameof(SHA512));
                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }

        public bool VerifySignature(string publicKeyPath, byte[] hashedDocument, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm(nameof(SHA512));
                return rsaDeformatter.VerifySignature(hashedDocument, signature);
            }
        }
    }
}
