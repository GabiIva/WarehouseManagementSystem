using System.Collections.Generic;
using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Infrastructure
{
    public class HistoryManager
    {
        private Stack<WarehouseMemento> _history = new Stack<WarehouseMemento>();

        public void SaveState(List<WarehouseComponent> state)
        {
            _history.Push(new WarehouseMemento(state));
        }

        public WarehouseMemento Undo()
        {
            if (_history.Count > 0)
            {
                return _history.Pop();
            }
            return null;
        }
    }
}