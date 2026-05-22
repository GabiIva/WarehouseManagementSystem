using System;
using System.Collections.Generic;

namespace WarehouseManagementSystem.Infrastructure
{
    public static class LoginService
    {
        private static Dictionary<string, string> users = new Dictionary<string, string>()
        {
            { "admin", "1234" },
            { "worker", "password" }
        };

        public static void ShowLoginMenu()
        {
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine("\n ============================================");
                Console.WriteLine("          СИСТЕМА ЗА ВХОД НА СЛУЖИТЕЛИ       ");
                Console.WriteLine(" ============================================");
                Console.WriteLine("\n  1) Вход в системата (Login)");
                Console.WriteLine("  2) Регистрация на нов работник (Register)");
                Console.WriteLine("  3) Изход от програмата");
                Console.WriteLine("  --------------------------------------------");
                Console.Write("  >> Избор: ");

                string choice = Console.ReadLine();

                if (choice == "1") // ЛОГИН
                {
                    Console.Clear();
                    Console.WriteLine("\n --- ВХОД УСЛУЖИТЕЛ ---");
                    Console.Write(" Потребителско име: ");
                    string username = Console.ReadLine().Trim();
                    Console.Write(" Парола: ");
                    string password = Console.ReadLine().Trim();

                    if (users.ContainsKey(username) && users[username] == password)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n Успешен вход! Добре дошли в склада.");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(" Натиснете клавиш за достъп до менюто...");
                        Console.ReadKey();
                        return; // Пуска потребителя напред
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n ❌ Грешно име или парола!");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.ReadKey();
                    }
                }
                else if (choice == "2") // РЕГИСТРАЦИЯ
                {
                    Console.Clear();
                    Console.WriteLine("\n --- РЕГИСТРАЦИЯ НА НОВ СЛУЖИТЕЛ ---");
                    Console.Write(" Въведете ново потребителско име: ");
                    string newUsername = Console.ReadLine().Trim();

                    if (users.ContainsKey(newUsername))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" ❌ Това потребителско име вече е заето!");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.ReadKey();
                    }
                    else if (string.IsNullOrEmpty(newUsername))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(" ❌ Името не може да бъде празно!");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Write(" Въведете парола: ");
                        string newPassword = Console.ReadLine().Trim();

                        users.Add(newUsername, newPassword);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n ✅ Успешна регистрация! Вече можете да влезете.");
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.ReadKey();
                    }
                }
                else if (choice == "3") // ИЗХОД
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}
