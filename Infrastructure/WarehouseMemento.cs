using System.Collections.Generic;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Infrastructure
{
    public class WarehouseMemento
    {
        public List<WarehouseComponent> State { get; private set; }

        public WarehouseMemento(List<WarehouseComponent> state)
        {
            this.State = new List<WarehouseComponent>(state);
        }
    }
}
