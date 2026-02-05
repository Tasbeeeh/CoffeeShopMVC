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

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null)
                _context.Orders.Remove(order);
        }

        public void Edit(Order order)
        {
            _context.Orders.Update(order);
        }

        public List<Order> GetAll()
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.Cart)
                .ToList();
        }

        public Order GetById(int id)
        {
            return _context.Orders.Find(id)!;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
