using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Strategies
{
    public class FragileShipping : IShippingStrategy
    {
        public decimal CalculateTotal(Product product)
        {
            return (product.Price * product.Quantity) * 1.15m;
        }
    }
}