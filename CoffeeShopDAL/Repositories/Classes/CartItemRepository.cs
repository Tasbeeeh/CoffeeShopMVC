using CoffeeShopDAL.Data;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Repositories.Classes
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly CoffeeShopDbContext _context;

        public CartItemRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public void Add(CartItem item)
        {
            _context.CartItems.Add(item);
        }

        public void Delete(CartItem item)
        {
            _context.CartItems.Remove(item);
        }

        public CartItem? Get(int cartId, int productId)
        {
            return _context.CartItems
                .FirstOrDefault(ci=>ci.CartId==cartId&&ci.ProductId==productId);
        }

        public List<CartItem> GetAll(int cartId)
        {
            return _context.CartItems
                .Include(p=>p.Product)
                .Where(c=>c.CartId == cartId)
                .ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(CartItem item)
        {
            _context.CartItems.Update(item);
        }
    }
}
