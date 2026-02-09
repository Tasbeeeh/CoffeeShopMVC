using CoffeeShopBLL.ModelVMs.Order;
using CoffeeShopBLL.ModelVMs.OrderItem;
using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Mapping
{
    public static class OrderItemMapping
    {
        public static OrderVM ToVM(this Order order)
        {
            return new OrderVM
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Items = order.OrderItems.Select(oi => new OrderItemVM
                {
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }

    }
}
