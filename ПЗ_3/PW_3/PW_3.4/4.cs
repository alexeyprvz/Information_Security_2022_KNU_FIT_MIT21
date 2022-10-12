using System;
using System.Text;
using System.Security.Cryptography;

Console.Write("Choose command: " +
    "\nl - login" +
    "\nr - register" +
    "\ne - exit" +
    "\n->");
char c = Convert.ToChar(Console.ReadLine());

string path = "..\\..\\..\\..\\.\\users\\";
string ext = ".txt";


string login;
string password;
string key = "MIT20222022MIT";
string filename;
string filepath;
string hashpassword;

switch (c)
{
    case 'l':
        Console.Write("Enter login: ");
        login = Console.ReadLine();

        filename = login + ext.ToString();
        filepath = path + filename.ToString();

        Console.Write("Enter password: ");
        password = Console.ReadLine();
        password = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password), Encoding.Unicode.GetBytes(key)));

        try
        {
            using (StreamReader read = File.OpenText(filepath))
                hashpassword = read.ReadLine();

            if (password == hashpassword)
            {
                Console.WriteLine("LOGIN SUCCESSFUL");
            }
            else
            {
                Console.WriteLine("LOGIN FAILED");
            }
        }
        catch
        {
            Console.WriteLine("USER DOES NOT EXIST");
        }
        break;

    case 'r':
        Console.Write("Enter login: ");
        login = Console.ReadLine();
        Console.Write("Enter password: ");
        password = Console.ReadLine();

        password = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password), Encoding.Unicode.GetBytes(key)));
        filename = login + ext.ToString();
        filepath = path + filename.ToString();

        using (StreamWriter write = new StreamWriter(filepath))
        {
            write.WriteLine(password);
        };
        Console.WriteLine("\nDONE");
        break;

    case 'e':
    default:
        Console.WriteLine("\nExit");
        break;
}

class hash
{
    public static byte[] ComputeHash(byte[] toBeHashed, byte[] key)
    {
        using (var hmacsha512 = new HMACSHA512(key))
        {
            return hmacsha512.ComputeHash(toBeHashed);
        }
    }
}