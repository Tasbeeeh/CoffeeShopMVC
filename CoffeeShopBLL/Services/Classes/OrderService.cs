using CoffeeShopBLL.Mapping;
using CoffeeShopBLL.ModelVMs.Order;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShopBLL.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Add(OrderVM orderVM)
        {
            var order = new Order
            {
                TotalPrice = orderVM.TotalPrice,
                OrderDate = orderVM.OrderDate,
                OrderStatus = orderVM.OrderStatus,
                UserId = orderVM.UserId,
                CartId = orderVM.CartId
            };

            _orderRepository.Add(order);
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }

        public void Edit(OrderVM orderVM)
        {
            var order = new Order
            {
                Id = orderVM.Id,
                TotalPrice = orderVM.TotalPrice,
                OrderDate = orderVM.OrderDate,
                OrderStatus = orderVM.OrderStatus,
                UserId = orderVM.UserId,
                CartId = orderVM.CartId
            };

            _orderRepository.Edit(order);
        }
        public List<OrderVM> GetAll()
        {
            return _orderRepository.GetAll()
                                   .Select(o => o.orderVM())
                                   .ToList();
        }
        public OrderVM GetById(int id)
        {
            var order = _orderRepository.GetById(id);
            return order.orderVM();
        }
        public int Save()
        {
            return _orderRepository.Save();
        }
    }
}