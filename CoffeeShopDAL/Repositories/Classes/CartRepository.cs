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
    public class CartRepository : ICartRepository
    {
        private readonly CoffeeShopDbContext _context;

        public CartRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public void ClearCart(int CartId)
        {
            var cart =  _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefault(c => c.Id == CartId);

            if (cart != null && cart.CartItems.Any())
            {
                _context.CartItems.RemoveRange(cart.CartItems);
            }
        }

        public void CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        public void DeleteCart(int CartId)
        {
            var cart = GetCartById(CartId);
            if(cart != null)
            {
                _context.Carts.Remove(cart);
            }
        }

        public Cart? GetCartById(int CartId)
        {
            return _context.Carts
                .FirstOrDefault(c=>c.Id == CartId);
        }

        public Cart? GetCartWithItems(string UserId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(p=>p.Product)
                //.Include(c => c.Voucher)
                .FirstOrDefault(c=>c.UserId == UserId);
        }

        public Cart? GetUserCart(string UserId)
        {
            return _context.Carts.FirstOrDefault(c=>c.UserId == UserId);
        }
        public Cart? GetCartWithItemsByCartId(int cartId)
        {
            return _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.Id == cartId);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}

