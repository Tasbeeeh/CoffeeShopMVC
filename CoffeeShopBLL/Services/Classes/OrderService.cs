using CoffeeShopBLL.Mapping;
using CoffeeShopBLL.ModelVMs.CartItem;
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

        public List<OrderVM> GetUserOrders(string userId)
        {
            return _orderRepository.GetOrdersByUserId(userId)
                        .Select(o => o.orderVM())
                        .ToList();
                }
        public OrderDetailsVM GetOrderDetails(int orderId)
        {
            var order = _orderRepository.GetOrderWithItems(orderId);

            if (order == null) return null;

            return new OrderDetailsVM
            {
                OrderId = order.Id,
                CreatedAt = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Items = order.Cart.CartItems.Select(ci => new CartItemVM
                {
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice
                }).ToList()
            };
        }


        public int Save()
        {
            return _orderRepository.Save();
        }
    }
}