using System.Security.Cryptography;

var rnd = new RNGCryptoServiceProvider();
var rndNum = new byte[10];
rnd.GetBytes(rndNum);
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(rndNum[i]);
}