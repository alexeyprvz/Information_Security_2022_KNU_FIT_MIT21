using System;
using System.Text;
using System.Security.Cryptography;

const string strForHash1 = "Hello world!";
const string strForHash2 = "KNU is the best!";
const string strForHash3 = "Ukraine brave!";

var md5ForStr1 = hash.ComputeHashMd5(Encoding.Unicode.GetBytes(strForHash1));
var sha256ForStr2 = hash.ComputeHashSha256(Encoding.Unicode.GetBytes(strForHash2));

Guid guid1 = new Guid(md5ForStr1);

hash.print_MD5(strForHash1, md5ForStr1, guid1);
hash.print_SHA(strForHash1, sha256ForStr2);

class hash
{
    public static byte[] ComputeHashMd5(byte[] dataForHash)
    {
        using (var md5 = MD5.Create())
        {
            return md5.ComputeHash(dataForHash);
        }
    }

    public static void print_MD5(string strForHash, byte[] HashForStr, Guid guid)
    {
        Console.WriteLine("Str: " + strForHash);
        Console.WriteLine("Hash MD5: " + (Convert.ToBase64String(HashForStr)));
        Console.WriteLine("GUID: " + guid + "\n");
    }

    public static byte[] ComputeHashSha256(byte[] toBeHashed)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(toBeHashed);
        }
    }

    public static void print_SHA(string strForHash, byte[] HashForStr)
    {
        Console.WriteLine("Str: " + strForHash);
        Console.WriteLine("Hash SHA: " + (Convert.ToBase64String(HashForStr)) + "\n");
    }
}