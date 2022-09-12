using System.Security.Cryptography;

Console.Write("Enter lenght: ");
int length = Convert.ToInt32(Console.ReadLine());

var rnd = new RNGCryptoServiceProvider();
var rndNum = new byte[length];
rnd.GetBytes(rndNum);

for (int i = 0; i < length; i++)
{
    Console.Write(rndNum[i]);
}