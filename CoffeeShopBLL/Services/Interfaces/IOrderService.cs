using System;
using CoffeeShopBLL.ModelVMs.Order;
using System.Collections.Generic;

namespace CoffeeShopBLL.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderVM> GetAll();
        OrderVM GetById(int id);
        void Add(OrderVM orderVM);
        void Edit(OrderVM orderVM);
        void Delete(int id);
        int Save();
    }
}