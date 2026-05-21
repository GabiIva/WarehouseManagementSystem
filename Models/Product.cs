using System;

namespace WarehouseManagementSystem.Models
{
    public class Product : WarehouseComponent
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsFragile { get; set; }

        public Product(string name, decimal price, int quantity, bool isFragile)
        {
            // ПРИНУДИТЕЛНО ЗАПИСВАНЕ
            this.Name = string.IsNullOrWhiteSpace(name) ? "Неизвестен" : name;
            this.Price = price;
            this.Quantity = quantity;
            this.IsFragile = isFragile;
        }

        public override void Display(int indent)
        {
            string indentation = new string(' ', indent);

            // Начало на реда
            Console.Write($"{indentation}- Продукт: ");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(this.Name.PadRight(15));

            // Ако е чупливо, ще добавим червен надпис
            if (IsFragile)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" [ЧУПЛИВО]");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" [СТАНДАРТ]");
            }

            // Цената и наличността в нормален цвят
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($" | Цена: {Price:F2} EUR | Наличност: {Quantity} бр.");
        }
    }
}