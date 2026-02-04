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

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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

        public void PlaceOrder(string UserId)
        {
            throw new NotImplementedException();
        }

        //public void PlaceOrder(string userId)
        //{
        //    var cart = await _cartRepo.GetCartWithItems(userId);

        //    if (cart == null || !cart.CartItems.Any())
        //        throw new Exception("Cart is Empty!");

        //    decimal total = cart.CartItems.Sum(x =>
                //x.Quantity * x.Product.Price);

        //    Order order = new Order()
        //    {
        //        UserId = userId,
        //        OrderDate = DateTime.Now,
        //        TotalPrice = total,
        //        OrderStatus = OrderStatus,
        //        CartId = cart.Id
        //    };

        //    await _orderRepo.AddOrder(order);

        //    await _cartRepo.ClearCart(cart.Id);
        //}

    }
}