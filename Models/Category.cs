using System;
using System.Collections.Generic;

namespace WarehouseManagementSystem.Models
{
    public class Category : WarehouseComponent
    {
        public List<WarehouseComponent> Children { get; set; } = new List<WarehouseComponent>();

        public Category(string name)
        {
            this.Name = name;
        }

        public void Add(WarehouseComponent component)
        {
            Children.Add(component);
        }

        public bool Remove(string name)
        {
            var item = Find(name);
            if (item != null && item != this)
            {
                return Children.Remove(item);
            }
            return false;
        }

        public override void Display(int indent)
        {
            string indentation = new string(' ', indent);

            Console.ForegroundColor = ConsoleColor.Yellow; // Категориите ще са жълти
            Console.WriteLine($"{indentation}📂 КАТЕГОРИЯ: {this.Name.ToUpper()}");
            Console.ForegroundColor = ConsoleColor.Black;

            foreach (var child in Children)
            {
                child.Display(indent + 4);
            }
        }

        public override WarehouseComponent Find(string name)
        {
            if (this.Name != null && this.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                return this;

            foreach (var child in Children)
            {
                var found = child.Find(name);
                if (found != null) return found;
            }
            return null;
        }
    }
}