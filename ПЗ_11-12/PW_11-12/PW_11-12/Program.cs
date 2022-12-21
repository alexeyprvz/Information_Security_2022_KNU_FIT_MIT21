using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;

namespace PW_11_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char start_choice;
            string login;
            string password;
            int role_num;
            int check_access_num;
            string check_access;
            int choice;
            string[] roles =
            {
                "admins",
                "programmers",
                "security_guards",
                "accountants"
            };

            do
            {
                Console.Clear();
                Console.Write("l - Login\n" +
                    "r - register\n" +
                    "e - exit\n" +
                    " -> ");
                start_choice = Convert.ToChar(Console.ReadLine());

                if (start_choice == 'l')
                {
                    if (Protector.AmountOfUsers() != 0)
                    {
                        Console.Clear();
                        Console.WriteLine("     #LOGIN#     \n");
                        Console.Write("Enter login: ");
                        login = Console.ReadLine();
                        Console.Write("Enter password: ");
                        password = Console.ReadLine();

                        if (!Protector.CheckPassword(login, password))
                        {
                            Console.ReadKey();
                            continue;
                        }
                        Protector.LogIn(login, password);

                        Console.Write("Access to materials:\n" +
                            "1 - for Admins\n" +
                            "2 - for Programmers\n" +
                            "3 - for Security guards\n" +
                            "4 - for Accountants\n" +
                            " -> ");
                        check_access_num = Convert.ToInt32(Console.ReadLine());
                        check_access = roles[check_access_num - 1];
                        CheckAccess.GetAccess(check_access);
                    }
                    else
                    {
                        Console.WriteLine("NO EXIST USERS TO LOGIN");
                        Console.ReadKey();
                    }
                   
                }

                if (start_choice == 'r')
                {
                    Console.Clear();
                    Console.WriteLine("     #REGISTRATION#     \n");
                    ContinueRegistration:
                    Console.Write("Enter login: ");
                    login = Console.ReadLine();
                    if (Protector.IfUserExist(login))
                    {
                        Console.WriteLine("THIS USER ALREADY EXIST");
                        Console.Write("Do you want to login or continue registration?\n" +
                            "1 - exit and login with this username\n" +
                            "2 - continue registration\n" +
                            " -> ");
                        choice = Convert.ToInt32(Console.ReadLine());
                        if (choice == 1) continue;
                        if (choice == 2) goto ContinueRegistration;
                    }
                    Console.Write("Enter password: ");
                    password = Console.ReadLine();
                    Console.Write("Choose role:\n" +
                        "1 - Admins\n" +
                        "2 - Programmers\n" +
                        "3 - Security guards\n" +
                        "4 - Accountants\n" +
                        " -> ");
                    role_num = Convert.ToInt32(Console.ReadLine());
                    string[] role = { roles[role_num - 1] };
                    Protector.Register(login, password, role);
                    Console.WriteLine("REGISTRATION SUCCESSFUL");
                    Console.ReadKey();
                }

                if (start_choice == 'e')
                {
                    Console.WriteLine("\nEXIT");
                    break;
                }

            } while (true);
        }
    }

    public class Hashing
    {
        public static byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var rnd = new byte[32];
                randomNumberGenerator.GetBytes(rnd);
                return rnd;
            }
        }

        public static byte[] HashPassword(string toBeHashed, byte[] salt, int numOfRounds)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numOfRounds, HashAlgorithmName.SHA256))
            {
                return rfc2898.GetBytes(32);
            }
        }
    }
    public class User
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string[] Roles { get; set; }
    }
    public class Protector
    {
        private static Dictionary<string, User> Users = new Dictionary<string, User>();

        private static int salt_length = 32;
        private static int NumberOfRoundes = 100000;

        public static User Register(string username, string password, string[] roles)
        {
            byte[] salt = Hashing.GenerateSalt();
            string saltStr = Convert.ToBase64String(salt);
            byte[] passwordHash = Hashing.HashPassword(password, salt, NumberOfRoundes);
            string passwordHashStr = Convert.ToBase64String(passwordHash);
            var NewUser = new User
            {
                Login = username,
                Salt = saltStr,
                PasswordHash = passwordHashStr,
                Roles = roles
            };
            Users.Add(NewUser.Login, NewUser);
            return NewUser;
        }
        public static bool CheckPassword(string username, string password)
        {
            //перевірка логіна у словнику
            if (!IfUserExist(username))
            {
                Console.WriteLine("NO EXIST USERS WITH THIS LOGIN");
                return false;
            }
            var user = Users[username];
            var salt = new byte[salt_length];
            salt = Convert.FromBase64String(user.Salt);
            var HashToCompare = Convert.ToBase64String(Hashing.HashPassword(password, salt, NumberOfRoundes));
            //співставлення хешів  
            if (HashToCompare != user.PasswordHash)
            {
                Console.WriteLine("WRONG PASSWORD");
                return (false);
            }
            return (true);
        }
        public static void LogIn(string userName, string password)
        {
            // Перевірка пароля
            if (CheckPassword(userName, password))
            {
                // Створюється екземпляр автентифікованого користувача
                var identity = new GenericIdentity(userName, "OIBAuth");
                // Виконується прив’язка до ролей, до яких належить користувач
                var principal = new GenericPrincipal(identity, Users[userName].Roles);
                // Створений екземпляр автентифікованого користувача з відповідними
                // ролями присвоюється потоку, в якому виконується програма
                System.Threading.Thread.CurrentPrincipal = principal;
                Console.WriteLine("LOGIN SUCCESSFUL");
            }
        }

        public static int AmountOfUsers()
        {
            return Users.Count();
        }

        public static bool IfUserExist(string username)
        {
            return Users.ContainsKey(username);
        }
    }
    
    public class CheckAccess
    {
        public static void OnlyForAdmins()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("admins"))
            {
                throw new SecurityException("User must be a member of Admins to access these materials.");
            }
            Console.WriteLine("You have access to all materials for Admins");
        }
        public static void OnlyForProgrammers()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("programmers"))
            {
                throw new SecurityException("User must be a member of Programmers to access these materials.");
            }
            Console.WriteLine("You have access to all materials for Programmers");
        }
        public static void OnlyForSecurityGuards()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("security_guards"))
            {
                throw new SecurityException("User must be a member of Security guards to access these materials.");
            }
            Console.WriteLine("You have access to all materials for Security guards");
        }
        public static void OnlyForAccountants()
        {
            if (Thread.CurrentPrincipal == null)
            {
                throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
            }
            if (!Thread.CurrentPrincipal.IsInRole("accountants"))
            {
                throw new SecurityException("User must be a member of Accountants to access these materials.");
            }
            Console.WriteLine("You have access to all materials for Accountants");
        }

        public static void GetAccess(string opt)
        {
            if (opt == "admins")
            {
                try
                {
                    OnlyForAdmins();
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                    Console.ReadKey();
                }
            }
            if (opt == "programmers")
            {
                try
                {
                    OnlyForProgrammers();
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                    Console.ReadKey();
                }
            }
            if (opt == "security_guards")
            {
                try
                {
                    OnlyForSecurityGuards();
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                    Console.ReadKey();
                }
            }
            if (opt == "accountants")
            {
                try
                {
                    OnlyForAccountants();
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }
    }
}
