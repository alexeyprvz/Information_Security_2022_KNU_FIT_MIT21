using System;
using System.Text;
using System.Security.Cryptography;

Console.Write("Enter text: ");
string strForHash = Console.ReadLine();
var bstrForHash = Encoding.Unicode.GetBytes(strForHash);

var md5ForStr = hash.ComputeHash(bstrForHash, "md5");
Guid guid = new Guid(md5ForStr);
Console.WriteLine("\nHash MD5: " + (Convert.ToBase64String(md5ForStr)));
Console.WriteLine("GUID: " + guid + "\n");

var sha1ForStr = hash.ComputeHash(bstrForHash, "sha1");
Console.WriteLine("Hash SHA1: " + (Convert.ToBase64String(sha1ForStr)));

var sha256ForStr = hash.ComputeHash(bstrForHash, "sha256");
Console.WriteLine("Hash SHA256: " + (Convert.ToBase64String(sha256ForStr)));

var sha384ForStr = hash.ComputeHash(bstrForHash, "sha384");
Console.WriteLine("Hash SHA384: " + (Convert.ToBase64String(sha384ForStr)));

var sha512ForStr = hash.ComputeHash(bstrForHash, "sha512");
Console.WriteLine("Hash SHA512: " + (Convert.ToBase64String(sha512ForStr) + "\n"));

class hash
{
    public static byte[] ComputeHash(byte[] toBeHashed, string metod)
    {
        switch (metod)
        {
            case "md5":
                using (var md5 = MD5.Create())
                {
                    return md5.ComputeHash(toBeHashed);
                }
                break;

            case "sha1":
                using (var sha1 = SHA1.Create())
                {
                    return sha1.ComputeHash(toBeHashed);
                }
                break;

            case "sha256":
                using (var sha256 = SHA256.Create())
                {
                    return sha256.ComputeHash(toBeHashed);
                }
                break;

            case "sha384":
                using (var sha384 = SHA384.Create())
                {
                    return sha384.ComputeHash(toBeHashed);
                }
                break;

            case "sha512":
                using (var sha512 = SHA512.Create())
                {
                    return sha512.ComputeHash(toBeHashed);
                }
                break;
        }
        return toBeHashed;
    }
}