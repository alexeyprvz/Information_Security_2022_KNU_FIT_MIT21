using System;
using System.Text;

string pathenc = "..\\..\\..\\..\\.\\files\\encmsg.dat";
string text;

using (StreamReader read = File.OpenText(pathenc))
    text = read.ReadToEnd();

Console.WriteLine("\nEncrypted message: " + text);

Console.Write("\nEnter password: ");
string password = Console.ReadLine();

byte[] btext = new byte[text.Length];
btext = Encoding.UTF8.GetBytes(text);
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

Console.WriteLine("\nDecrypted message: " + Encoding.UTF8.GetString(bencmsg));