using System;
using CoffeeShopBLL.ModelVMs.Order;
using System.Collections.Generic;

namespace CoffeeShopBLL.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderVM> GetAll();
        OrderVM? GetById(int id);
        void Delete(int id);
        void PlaceOrder(string UserId);
    }
}