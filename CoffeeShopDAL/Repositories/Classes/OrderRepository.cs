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
                .Include(o => o.User)
                //.Include(o => o.Cart)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public Order? GetOrderById(int id)
        {
            return _context.Orders
                .Include(o=>o.User)
                .SingleOrDefault(o=>o.Id==id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
