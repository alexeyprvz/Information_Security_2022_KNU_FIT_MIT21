using System;
using System.Text;

string path = "..\\..\\..\\..\\.\\files\\text.txt";
string pathenc = "..\\..\\..\\..\\.\\files\\encmsg.dat";

string text;

Console.WriteLine("Do you want to enter new message?\ny - yes\nn - no");
char c = Convert.ToChar(Console.ReadLine());

switch(c)
{
    case 'y':
        Console.WriteLine("Enter new message");
        text = Console.ReadLine();
        using (StreamWriter write = new StreamWriter(path))
            write.WriteLine(text);
        break;

    case 'n':
    default:
        using (StreamReader read = File.OpenText(path))
            text = read.ReadToEnd();
        break;

}
Console.WriteLine("\nMessage in file: " + text);

string password;
do
{
    Console.Write("Enter password: ");
    password = Console.ReadLine();
} while (password.Length == 0);

byte[] btext = Encoding.UTF8.GetBytes(text);
byte[] bpassword = Encoding.UTF8.GetBytes(password);
byte[] bencmsg = new byte[btext.Length];

for (int i = 0, j = 0; i < btext.Length; i++)
{
    if (j == bpassword.Length)
        j = 0;
    byte bencmsgel = (byte)(btext[i] ^ bpassword[j]);
    bencmsg[i] = bencmsgel;
    j++;
}

using (FileStream file = new FileStream(pathenc, FileMode.OpenOrCreate))
    file.Write(bencmsg);

Console.WriteLine("\nThe message was encrypted and saved!");

//string encmsg = Encoding.UTF8.GetString(bencmsg);
//Console.WriteLine("\nEncrypted message: " + encmsg);

//for (int i = 0, j = 0; i < btext.Length; i++)
//{
//    if (j == bpassword.Length)
//        j = 0;
//    byte bencmsgel = (byte)(bencmsg[i] ^ bpassword[j]);
//    bencmsg[i] = bencmsgel;
//}

//Console.WriteLine("\nDecrypted message: " + Encoding.UTF8.GetString(bencmsg));



