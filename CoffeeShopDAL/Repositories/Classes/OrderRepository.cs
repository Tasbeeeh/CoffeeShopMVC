using CoffeeShopDAL.Data;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShopDAL.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CoffeeShopDbContext _context;

        public OrderRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void DeleteOrder(int id)
        {
            var order = GetOrderById(id);
            if (order != null)
                _context.Orders.Remove(order);
        }

       

        public List<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(p=>p.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public Order? GetOrderById(int id)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(p => p.Product)
                .SingleOrDefault(o=>o.Id==id);
        }

        public List<Order> GetOrdersByUserId(string userId)
        {
            return _context.Orders
            .Where(o => o.User.Id == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToList();
        }
        public Order GetOrderWithItems(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
        .FirstOrDefault(o => o.Id == orderId);
        }


        public int Save()
        {
            return _context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
        }
    }
}
