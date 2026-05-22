using System;
using System.Collections.Generic;
using WarehouseManagementSystem.Models;
using WarehouseManagementSystem.Strategies;
using WarehouseManagementSystem.Infrastructure;

namespace WarehouseManagementSystem
{
    class Program
    {
        static Category root = new Category("Основен Склад");
        static HistoryManager history = new HistoryManager();

        static string filePath = "warehouse_data.txt";
        static FileService fileService = new FileService();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.BackgroundColor = ConsoleColor.Yellow;     
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Black;
            var loadedComponents = fileService.LoadFromFile(filePath);
            foreach (var component in loadedComponents)
            {
                root.Add(component);
            }

            WarehouseManagementSystem.Infrastructure.LoginService.ShowLoginMenu();

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine("\n ============================================");
                Console.WriteLine("     СИСТЕМА ЗА УПРАВЛЕНИЕ НА СКЛАД     ");
                Console.WriteLine(" ============================================");

                Console.WriteLine();
                Console.WriteLine("  --> Изберете опция от менюто (1 - 9):");
                Console.WriteLine("  --------------------------------------------");

                Console.WriteLine("  --- Продукти и Наличности ---");
                Console.WriteLine("   1) Добави продукт");
                Console.WriteLine("   2) Покажи инвентар");
                Console.WriteLine("   3) Редактирай продукт");
                Console.WriteLine("   4) Изтрий продукт");

                Console.WriteLine();

                Console.WriteLine("  --- Доставка и Статистика ---");
                Console.WriteLine("   5) Подготовка за доставка (Strategy)");
                Console.WriteLine("   6) Статистика на склада");
                Console.WriteLine("   7) Върни назад (Undo)");

                Console.WriteLine();

                Console.WriteLine("  --- Файлове и Изход ---");
                Console.WriteLine("   8) Запиши данните във файл");
                Console.WriteLine("   9) Изход");

                Console.WriteLine("  --------------------------------------------");
                Console.Write("  >> Избор: ");

                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        AddProductFlow();
                        break;

                    case "2":
                        Console.WriteLine("\n--- ТЕКУЩ ИНВЕНТАР ---");
                        root.Display(0);
                        Console.WriteLine("\nНатиснете клавиш за връщане...");
                        Console.ReadKey();
                        break;

                    case "3":
                        EditProductFlow();
                        break;

                    case "4":
                        DeleteProductFlow();
                        break;

                    case "5":
                        ProcessShipping();
                        break;

                    case "6":
                        ShowStatistics(); 
                        break;

                    case "7":
                        PerformUndo();
                        break;

                    case "8":
                        fileService.SaveToFile(filePath, root.Children);
                        break;

                    case "9":
                        fileService.SaveToFile(filePath, root.Children);
                        Console.WriteLine("Програмата се затваря. Довиждане!");
                        return; // Излиза от Main и затваря програмата

                    default:
                        Console.WriteLine("❌ Невалиден избор! Опитайте пак.");
                        System.Threading.Thread.Sleep(1000); 
                        break;
                }
            }
        }

        static void AddProductFlow()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n ============================================");
            Console.WriteLine("     ДОБАВЯНЕ НА НОВ ПРОДУКТ     ");
            Console.WriteLine(" ============================================");
            
            string name = "";
            while (true)
            {
                Console.Write("Въведете име на продукта: ");
                name = Console.ReadLine()?.Trim() ?? "";

                if (!string.IsNullOrEmpty(name))
                {
                    break; // Името е валидно, излизаме от цикъла
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Името на продукта не може да бъде празно!");
                Console.ForegroundColor = ConsoleColor.Black;
            }
            
            decimal price;
            while (true)
            {
                Console.Write("Въведете цена (в EUR): ");
                string priceInput = Console.ReadLine();

                if (decimal.TryParse(priceInput, out price) && price >= 0)
                {
                    break; 
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Невалидна цена! Моля, въведете положително число (напр. 24.99).");
                Console.ForegroundColor = ConsoleColor.Black;
            }

            int quantity;
            while (true)
            {
                Console.Write("Въведете количество: ");
                string qtyInput = Console.ReadLine();

                if (int.TryParse(qtyInput, out quantity) && quantity >= 0)
                {
                    break; 
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Невалидно количество! Моля, въведете цяло положително число (напр. 10).");
                Console.ForegroundColor = ConsoleColor.Black;
            }
            
            bool isFragile = false;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Write("Чуплив ли е продуктът? (да/не): ");
                string fragileInput = Console.ReadLine().ToLower().Trim();

                if (fragileInput == "да" || fragileInput == "yes")
                {
                    isFragile = true;
                    break;
                }
                else if (fragileInput == "не" || fragileInput == "no")
                {
                    isFragile = false;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Невалиден отговор! Моля, въведете 'да' или 'не'.");
                Console.ForegroundColor = ConsoleColor.Black;
            }
            
            Product newProduct = new Product(name, price, quantity, isFragile);

            root.Add(newProduct); 

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Продуктът '{name}' беше добавен успешно в паметта!");
            Console.ForegroundColor = ConsoleColor.Black;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("⚠️ Забележка: Не забравяйте да изберете Опция 8 от менюто, за да го запишете във файла!");
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("\nНатиснете клавиш за връщане към менюто...");
            Console.ReadKey();
        }

        static void EditProductFlow()
        {
            Console.Write("Име на продукт за редакция: ");
            string name = Console.ReadLine();
            var item = root.Find(name) as Product;

            if (item != null)
            {
                history.SaveState(new List<WarehouseComponent>(root.Children));
                try
                {
                    Console.Write($"Нова цена (сегашна: {item.Price}): ");
                    item.Price = decimal.Parse(Console.ReadLine());
                    Console.Write($"Ново количество (сегашно: {item.Quantity}): ");
                    item.Quantity = int.Parse(Console.ReadLine());
                    Console.WriteLine("✅ Редактирано успешно!");
                }
                catch { Console.WriteLine("❌ Невалидни данни!"); }
            }
            else Console.WriteLine("❌ Не е намерен.");
        }

        static void DeleteProductFlow()
        {
            Console.Write("Име за изтриване: ");
            string name = Console.ReadLine();
            history.SaveState(new List<WarehouseComponent>(root.Children));

            if (root.Remove(name)) Console.WriteLine("✅ Изтрито!");
            else { Console.WriteLine("❌ Не е намерен."); history.Undo(); }
        }

        static void ProcessShipping()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n ============================================");
            Console.WriteLine("     ПОДГОТОВКАЗА ДОСТАВКА     ");
            Console.WriteLine(" ============================================");
            Console.Write("Въведете име на продукта: ");

            string input = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Невалиден вход!");
                Console.ReadKey();
                return;
            }

            Product foundProduct = null;

            foreach (var item in root.Children)
            {
                if (item is Product p)
                {
                    string cleanDatabaseName = System.Text.RegularExpressions.Regex.Replace(p.Name, @"\x1B\[[^@-_]*[0-?]*[ -/]*[@-~]", "");

                    cleanDatabaseName = cleanDatabaseName.Trim().ToLower();
                    string cleanInput = input.Trim().ToLower();

                    if (cleanDatabaseName == cleanInput || cleanDatabaseName.Contains(cleanInput))
                    {
                        foundProduct = p;
                        break;
                    }
                }
            }

            if (foundProduct != null)
            {
                IShippingStrategy strategy = foundProduct.IsFragile ? new FragileShipping() : new StandardShipping();
                decimal total = strategy.CalculateTotal(foundProduct);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✅ Продуктът е намерен успешно!");
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine($"📦 Артикул: {foundProduct.Name}");
                Console.WriteLine($"💰 Крайна цена с доставка: {total:F2} EUR");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Продуктът ПАК не е намерен!");
                Console.ForegroundColor = ConsoleColor.Black;

                Console.WriteLine("\nНалични опции в склада:");
                foreach (var item in root.Children.OfType<Product>())
                {
                    Console.WriteLine($"- {item.Name}");
                }
            }

            Console.WriteLine("\nНатиснете клавиш за връщане...");
            Console.ReadKey();
        }

        static void ShowStatistics()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n ============================================");
            Console.WriteLine("     СТАТИСТИКА НА СКЛАДА     ");
            Console.WriteLine(" ============================================");

            int totalQty = 0;
            decimal totalValue = 0;

            List<string> uniqueProductNames = new List<string>();

            foreach (var node in root.Children)
            {
                if (node is Product p)
                {
                    string cleanName = System.Text.RegularExpressions.Regex.Replace(p.Name, @"\x1B\[[^@-_]*[0-?]*[ -/]*[@-~]", "").Trim();

                    if (uniqueProductNames.Contains(cleanName))
                    {
                        continue;
                    }

                    uniqueProductNames.Add(cleanName);
                    totalQty += p.Quantity;
                    totalValue += (p.Price * p.Quantity);
                }
            }

            Console.WriteLine($"Общо уникални продукти: {uniqueProductNames.Count}");
            Console.WriteLine($"Обща наличност в склада: {totalQty} бр.");
            Console.WriteLine($"Обща стойност на инвентара: {totalValue:F2} EUR");
            Console.WriteLine("\nНатиснете клавиш за връщане...");
            Console.ReadKey();
        }

        static void PerformUndo()
        {
            var previous = history.Undo();
            if (previous != null)
            {
                root.Children = previous.State;
                Console.WriteLine("⏪ Промяната е отменена!");
            }
            else Console.WriteLine("ℹ️ Няма действия за връщане.");
        }
    }
}
