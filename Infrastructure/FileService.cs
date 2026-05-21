using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Infrastructure
{
    public class FileService
    {
        // Метод за записване на данните
        public void SaveToFile(string filePath, List<WarehouseComponent> components)
        {
            try
            {
                // Използваме Encoding.UTF8 за правилно записване на кирилица
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (var component in components)
                    {
                        if (component is Product p)
                        {
                            // Записваме формат: Тип|Име|Цена|Количество|Чупливост
                            writer.WriteLine($"Product|{p.Name}|{p.Price}|{p.Quantity}|{p.IsFragile}");
                        }
                        else if (component is Category c)
                        {
                            // Ако имаш категории, записваме и тях
                            writer.WriteLine($"Category|{c.Name}");
                        }
                    }
                }
                Console.WriteLine("✅ Данните бяха успешно записани във файла.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ГРЕШКА при запис: {ex.Message}");
            }
        }

        // Метод за зареждане на данните
        public List<WarehouseComponent> LoadFromFile(string filePath)
        {
            List<WarehouseComponent> components = new List<WarehouseComponent>();

            if (!File.Exists(filePath))
            {
                return components;
            }

            try
            {
                // Използваме Encoding.UTF8 за правилно четене на кирилица
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 0) continue;

                        if (parts[0] == "Product" && parts.Length == 5)
                        {
                            string name = parts[1];
                            decimal price = decimal.Parse(parts[2]);
                            int qty = int.Parse(parts[3]);
                            bool frag = bool.Parse(parts[4]);

                            components.Add(new Product(name, price, qty, frag));
                        }
                        else if (parts[0] == "Category" && parts.Length == 2)
                        {
                            components.Add(new Category(parts[1]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ГРЕШКА при зареждане: {ex.Message}");
            }

            return components;
        }
    }
}