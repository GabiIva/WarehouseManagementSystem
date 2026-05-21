using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Strategies
{
    public class StandardShipping : IShippingStrategy
    {
        public decimal CalculateTotal(Product product)
        {
            return (product.Price * product.Quantity) * 1.05m;
        }
    }
}