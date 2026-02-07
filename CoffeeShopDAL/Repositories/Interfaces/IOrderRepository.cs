using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Repositories.Interfaces
{
    public interface IOrderRepository 
    {
        List<Order> GetAll();
        Order? GetOrderById(int id);
        void AddOrder(Order order); //manually for admins
        void DeleteOrder(int id);
        void UpdateOrder(Order order);
        int Save();
    }
}