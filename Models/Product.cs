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
            this.Name = string.IsNullOrWhiteSpace(name) ? "Íåèçâåñòåí" : name;
            this.Price = price;
            this.Quantity = quantity;
            this.IsFragile = isFragile;
        }

        public override void Display(int indent)
        {
            string indentation = new string(' ', indent);
            
            Console.Write($"{indentation}- Ïðîäóêò: ");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(this.Name.PadRight(15));

            if (IsFragile)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" [×ÓÏËÈÂÎ]");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" [ÑÒÀÍÄÀÐÒ]");
            }

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($" | Öåíà: {Price:F2} EUR | Íàëè÷íîñò: {Quantity} áð.");
        }
    }
}
