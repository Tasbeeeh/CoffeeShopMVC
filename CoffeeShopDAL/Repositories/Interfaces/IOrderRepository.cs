using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetOrdersByUserId(string userId);
        Order GetOrderWithItems(int orderId);

    }
}