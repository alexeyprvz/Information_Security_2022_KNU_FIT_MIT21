//using System;
//using System.Text;

//string pathenc = "..\\..\\..\\..\\.\\files\\encfile\\encfile5.dat";
//string text;

//using (StreamReader read = File.OpenText(pathenc))
//    text = read.ReadToEnd();

//byte[] btext = new byte[text.Length];
//btext = Encoding.UTF8.GetBytes(text);
//byte[] bdecmsg = new byte[btext.Length];
//byte[] bpassword;
//string example = "Mit21";
//byte[] bexample = Encoding.UTF8.GetBytes(example);

//while (true)
//{
//    bpassword = decrypting.GetPassword2();

//    for (int i = 0, j = 0; i < btext.Length; i++)
//    {
//        if (j == bpassword.Length)
//            j = 0;
//        byte bdecmsgel = (byte)(btext[i] ^ bpassword[j]);
//        bdecmsg[i] = bdecmsgel;
//        j++;
//    }
//    for (int i = 0; i < (btext.Length - 4); i++)
//    {
//        if (bdecmsg[i..(i+4)] == bexample)
//        {
//            Console.WriteLine("\nDecrypted message: " + Encoding.UTF8.GetString(bdecmsg) + "\n");
//            Console.ReadKey();
//        }
            
//    }
    
//}


//class decrypting
//{
//    public static byte[] GetPassword()
//    {
//        string password = "";
//        Random rand = new Random();
//        int randValue;
//        char letter;
//        for (int i = 0; i < 5; i++)
//        {
//            randValue = rand.Next(0, 26);
//            letter = Convert.ToChar(randValue + 65);
//            password = password + letter;
//        }
//        Console.WriteLine("Password:" + password);
//        byte[] bpassword = Encoding.UTF8.GetBytes(password);
//        return bpassword;
//    }
//    public static byte[] GetPassword2()
//    {
//        Random random = new Random();
//        var rString = "";
//        for (var i = 0; i < 5; i++)
//        {
//            rString += ((char)(random.Next(1, 26) + 64)).ToString().ToLower();
//        }
//        Console.WriteLine("Password:" + rString);
//        byte[] bpassword = Encoding.UTF8.GetBytes(rString);
//        return bpassword;
//    }
//}