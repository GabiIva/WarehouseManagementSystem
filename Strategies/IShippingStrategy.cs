using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Strategies
{
    public interface IShippingStrategy
    {
        decimal CalculateTotal(Product product);
    }
}