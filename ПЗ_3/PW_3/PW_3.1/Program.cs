using System;
using System.Text;
using System.Security.Cryptography;

Console.WriteLine("Enter text: ");
string strForHash = Console.ReadLine();
var bstrForHash = Encoding.Unicode.GetBytes(strForHash);

var md5ForStr = hash.ComputeHash(bstrForHash, "md5", bstrForHash);
Guid guid = new Guid(md5ForStr);
Console.WriteLine("\nHash MD5: " + (Convert.ToBase64String(md5ForStr)));
Console.WriteLine("GUID: " + guid + "\n");

var sha1ForStr = hash.ComputeHash(bstrForHash, "sha1", bstrForHash);
Console.WriteLine("Hash SHA1: " + (Convert.ToBase64String(sha1ForStr)));

var sha256ForStr = hash.ComputeHash(bstrForHash, "sha256", bstrForHash);
Console.WriteLine("Hash SHA256: " + (Convert.ToBase64String(sha256ForStr)));

var sha384ForStr = hash.ComputeHash(bstrForHash, "sha384", bstrForHash);
Console.WriteLine("Hash SHA384: " + (Convert.ToBase64String(sha384ForStr)));

var sha512ForStr = hash.ComputeHash(bstrForHash, "sha512", bstrForHash);
Console.WriteLine("Hash SHA512: " + (Convert.ToBase64String(sha512ForStr) + "\n"));

Console.WriteLine("Enter key: ");
string key = Console.ReadLine();
var bkey = Encoding.Unicode.GetBytes(key);

var md5hmacForStr = hash.ComputeHash(bstrForHash, "hmacmd5", bkey);
Console.WriteLine("Hash MD5 HMAC: " + (Convert.ToBase64String(md5hmacForStr)));

var sha1hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha1", bkey);
Console.WriteLine("Hash SHA1 HMAC: " + (Convert.ToBase64String(sha1hmacForStr)));

var sha256hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha256", bkey);
Console.WriteLine("Hash SHA256 HMAC: " + (Convert.ToBase64String(sha256hmacForStr)));

var sha384hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha384", bkey);
Console.WriteLine("Hash SHA384 HMAC: " + (Convert.ToBase64String(sha384hmacForStr)));

var sha512hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha512", bkey);
Console.WriteLine("Hash SHA512 HMAC: " + (Convert.ToBase64String(sha512hmacForStr)));



class hash
{
    public static byte[] ComputeHash(byte[] toBeHashed, string metod, byte[] key)
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

            case "hmacmd5":
                using (var hmacmd5 = new HMACMD5(key))
                {
                    return hmacmd5.ComputeHash(toBeHashed);
                }
                break;

            case "hmacsha1":
                using (var hmacsha1 = new HMACSHA1(key))
                {
                    return hmacsha1.ComputeHash(toBeHashed);
                }
                break;

            case "hmacsha256":
                using (var hmacsha256 = new HMACSHA256(key))
                {
                    return hmacsha256.ComputeHash(toBeHashed);
                }
                break;

            case "hmacsha384":
                using (var hmacsha384 = new HMACSHA384(key))
                {
                    return hmacsha384.ComputeHash(toBeHashed);
                }
                break;

            case "hmacsha512":
                using (var hmacsha512 = new HMACSHA512(key))
                {
                    return hmacsha512.ComputeHash(toBeHashed);
                }
                break;
        }
        return toBeHashed;
    }
}