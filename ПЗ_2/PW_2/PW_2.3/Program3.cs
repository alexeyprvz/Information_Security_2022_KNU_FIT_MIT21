//using System;
//using System.Text;

//string pathenc = "..\\..\\..\\..\\.\\files\\encfile\\encfile5.dat";
//string text;

//using (StreamReader read = File.OpenText(pathenc))
//    text = read.ReadToEnd();

////byte[] btext = new byte[text.Length];
////btext = Encoding.UTF8.GetBytes(text);
////byte[] bdecmsg = new byte[btext.Length];
////byte[] bpassword;
//string example = "Mit21";
////byte[] bexample = Encoding.UTF8.GetBytes(example);
//string ftext = "";
//string password;

//while (true)
//{
//    ftext = "";
//    password = decrypting.GetPassword();
//    Console.WriteLine("Password: " + password);

//    for (int i = 0, j = 0; i < text.Length; i++)
//    {
//        if (j < 5)
//            j = 0;
//        ftext += (char)(text[i] ^ password[j]);
//        j++;
//    }
//    Console.WriteLine("\nDecrypted message: " + ftext + "\n");  
    
//    for (int i = 0; i < (text.Length - 4); i++)
//    {
//        if (ftext[i..(i + 5)] == example)
//        {
//            Console.ReadKey();
//        }
//        //Console.WriteLine(ftext[i..(i + 5)]);
//    }
//    //Console.ReadKey();
//}


//class decrypting
//{
//    public static string GetPassword()
//    {
//        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//        var stringChars = new char[5];
//        var random = new Random();

//        for (int i = 0; i < 5; i++)
//        {
//            stringChars[i] = chars[random.Next(chars.Length)];
//        }

//        var finalString = new String(stringChars); ;
//        return finalString;
//    }
//}