using CoffeeShopBLL.Mapping;
using CoffeeShopBLL.ModelVMs.Order;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Entities.Enums;
using CoffeeShopDAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShopBLL.Services.Classes
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public void Add(OrderVM orderVM)
        {
            Order order = new Order()
            {
                Id = orderVM.Id,
                OrderDate = orderVM.OrderDate,
                OrderStatus = orderVM.OrderStatus,
                TotalPrice = orderVM.TotalPrice,
                UserId = orderVM.UserId,
                
            };
            _orderRepository.AddOrder(order);
            _orderRepository.Save();
        }

        public void Delete(int id)
        {
            _orderRepository.DeleteOrder(id);
        }

        public List<OrderVM> GetAll()
        {
           return _orderRepository.GetAll().Select(o=>o.orderVM()).ToList();
        }

        public OrderVM? GetById(int id)
        {
            return _orderRepository.GetOrderById(id).orderVM();
        }

      

        public void PlaceOrder(string userId)
        {
            var cart = _cartRepository.GetCartWithItems(userId);

            if (cart == null || !cart.CartItems.Any())
                throw new Exception("Cart is Empty!");

            decimal total = cart.CartItems.Sum(x =>
                x.Quantity * x.Product.Price);

            Order order = new Order()
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalPrice = total,
                OrderStatus = OrderStatus.Pending,
                
            };

             _orderRepository.AddOrder(order);

             _cartRepository.ClearCart(cart.Id);
        }

        public void Update(OrderVM orderVM)
        {
            Order order = new Order()
            {
                Id = orderVM.Id,
                OrderDate = orderVM.OrderDate,
                OrderStatus = orderVM.OrderStatus,
                TotalPrice = orderVM.TotalPrice,
                UserId = orderVM.UserId,

            };
            _orderRepository.UpdateOrder(order);
            _orderRepository.Save();
        }

        public void ChangeOrderStatus(int orderId, OrderStatus status)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order == null)
                throw new Exception("Order not found");

            order.OrderStatus = status;
            _orderRepository.Save();
        }

    }
}