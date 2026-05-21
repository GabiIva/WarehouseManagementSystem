using System;

namespace WarehouseManagementSystem.Models
{
    public abstract class WarehouseComponent
    {
        // Увери се, че пише точно това:
        public string Name { get; set; }

        public abstract void Display(int indent);

        public virtual WarehouseComponent Find(string name)
        {
            if (this.Name != null && this.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                return this;
            return null;
        }
    }
}