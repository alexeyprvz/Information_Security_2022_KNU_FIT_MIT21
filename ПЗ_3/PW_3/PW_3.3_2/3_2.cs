using System;
using System.Text;
using System.Security.Cryptography;

Console.Write("Enter text: ");
string strForHash = Console.ReadLine();
var bstrForHash = Encoding.Unicode.GetBytes(strForHash);

Console.Write("\nEnter Hash: ");
string strHash = Console.ReadLine();
var bHash = Encoding.Unicode.GetBytes(strHash);

Console.Write("\nEnter key: ");
string key = Console.ReadLine();
var bkey = Encoding.Unicode.GetBytes(key);

var md5hmacForStr = hash.ComputeHash(bstrForHash, "hmacmd5", bkey);
var sha1hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha1", bkey);
var sha256hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha256", bkey);
var sha384hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha384", bkey);
var sha512hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha512", bkey);

bool md5 = Convert.ToBase64String(md5hmacForStr) == strHash;
bool sha1 = Convert.ToBase64String(sha1hmacForStr) == strHash;
bool sha256 = Convert.ToBase64String(sha256hmacForStr) == strHash;
bool sha384 = Convert.ToBase64String(sha384hmacForStr) == strHash;
bool sha512 = Convert.ToBase64String(sha512hmacForStr) == strHash;

if (md5 || sha1 || sha256 || sha384 || sha512)
{
    Console.WriteLine("Your message authenticity is correct!");
}
else
{
    Console.WriteLine("Your message authenticity is wrong!");
}

class hash
{
    public static byte[] ComputeHash(byte[] toBeHashed, string metod, byte[] key)
    {
        switch (metod)
        {
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