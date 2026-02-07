using CoffeeShopBLL.ModelVMs.Order;
using CoffeeShopDAL.Entities.Enums;
using System;
using System.Collections.Generic;

namespace CoffeeShopBLL.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderVM> GetAll();
        OrderVM? GetById(int id);
        void Delete(int id);
        void Add(OrderVM orderVM);
        void Update(OrderVM orderVM);
        void PlaceOrder(string UserId);
        void ChangeOrderStatus(int orderId, OrderStatus status);

    }
}