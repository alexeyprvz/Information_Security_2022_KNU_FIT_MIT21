using System;
using System.Text;
using System.Security.Cryptography;

Console.Write("Enter text: ");
string strForHash = Console.ReadLine();
var bstrForHash = Encoding.Unicode.GetBytes(strForHash);

Console.Write("\nEnter key: ");
string key = Console.ReadLine();
var bkey = Encoding.Unicode.GetBytes(key);

Console.Write("\nChoose algoritm: " +
    "\n1 - hmacmd5" +
    "\n2 - hmacsha1" +
    "\n3 - hmacsha256" +
    "\n4 - hmacsha384" +
    "\n5 - hmacsha512" +
    "\n-> ");
char alg = Convert.ToChar(Console.ReadLine());

switch (alg)
{
    case '1':
        var md5hmacForStr = hash.ComputeHash(bstrForHash, "hmacmd5", bkey);
        Console.WriteLine("\nHash MD5 HMAC: " + (Convert.ToBase64String(md5hmacForStr)));
        break;

    case '2':
        var sha1hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha1", bkey);
        Console.WriteLine("\nHash SHA1 HMAC: " + (Convert.ToBase64String(sha1hmacForStr)));
        break;

    case '3':
        var sha256hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha256", bkey);
        Console.WriteLine("\nHash SHA256 HMAC: " + (Convert.ToBase64String(sha256hmacForStr)));
        break;

    case '4':
        var sha384hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha384", bkey);
        Console.WriteLine("\nHash SHA384 HMAC: " + (Convert.ToBase64String(sha384hmacForStr)));
        break;

    case '5':
        var sha512hmacForStr = hash.ComputeHash(bstrForHash, "hmacsha512", bkey);
        Console.WriteLine("\nHash SHA512 HMAC: " + (Convert.ToBase64String(sha512hmacForStr)));
        break;
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
