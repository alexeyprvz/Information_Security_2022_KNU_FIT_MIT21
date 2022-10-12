using System;
using System.Text;
using System.Security.Cryptography;

string hashtext = "po1MVkAE7IjUUwu61XxgNg==";

string password = "0000000";


for (int i = 0; i < 100000000; i++)
{
    if (i == 10) password = password.Remove(password.Length - 1);
    if (i == 100) password = password.Remove(password.Length - 1);
    if (i == 1000) password = password.Remove(password.Length - 1);
    if (i == 10000) password = password.Remove(password.Length - 1);
    if (i == 100000) password = password.Remove(password.Length - 1);
    if (i == 1000000) password = password.Remove(password.Length - 1);
    if (i == 10000000) password = password.Remove(password.Length - 1);
    string itrpassword = password;
    itrpassword = itrpassword + i.ToString();
    Console.WriteLine(itrpassword);
    byte[] bpassword = Encoding.Unicode.GetBytes(itrpassword);
    var tryhash = hash.ComputeHash(bpassword);
    string tryhashstr = Convert.ToBase64String(tryhash);

    if (hashtext == tryhashstr)
    {
        Console.WriteLine("Password: " + itrpassword);
        break;
    }
}

class hash
{
    public static byte[] ComputeHash(byte[] toBeHashed)
    {
        using (var md5 = MD5.Create())
        {
            return md5.ComputeHash(toBeHashed);
        }
    }
}