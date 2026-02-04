using CoffeeShopBLL.ModelVMs.Order;
using CoffeeShopDAL.Entities;

namespace CoffeeShopBLL.Mapping
{
    public static class OrderMapping
    {
        public static OrderVM orderVM(this Order order)
        {
            return new OrderVM
            {
                Id = order.Id,
                TotalPrice = order.TotalPrice,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                UserId = order.UserId,
                //CartId = order.CartId
            };
        }
    }
}