using CoffeeShopBLL.Mapping;
using CoffeeShopBLL.ModelVMs.Cart;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Classes
{
    public class CartService : ICartService
    {

        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public bool ClearCart(int cartId)
        {
            var cart =  _cartRepository.GetCartById(cartId);
            if (cart == null) return false;

             _cartRepository.ClearCart(cartId);

            cart.TotalPrice = 0;
             _cartRepository.Save();

            return true;
        }

        public CartViewModel CreateCart(string userId)
        {
            var cart = new Cart
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = 0
            };

            _cartRepository.CreateCart(cart);
             _cartRepository.Save();

            return cart.Cartviewmodel();
        }

        public bool DeleteCart(int cartId)
        {
            var cart = _cartRepository.GetCartById(cartId);
            if (cart == null) return false;

            _cartRepository.DeleteCart(cartId);

            _cartRepository.Save();

            return true;
        }

        public CartViewModel? GetCartById(int cartId)
        {
            var cart = _cartRepository.GetCartById(cartId);
            return cart == null ? null : cart.Cartviewmodel();
        }

        public CartViewModel? GetCartWithItems(string userId)
        {
            var cart = _cartRepository.GetCartWithItems(userId);
            if (cart == null) return null;

            if (cart.CartItems == null)
                cart.CartItems = new List<CartItem>();

            UpdateCartTotalPrice(cart); 
            _cartRepository.Save();     

            return cart.Cartviewmodel();
        }

        public CartViewModel GetUserCart(string userId)
        {
            var cart = _cartRepository.GetCartWithItems(userId);
            if (cart == null)
                return CreateCart(userId);

            UpdateCartTotalPrice(cart); 
            _cartRepository.Save();     

            return cart.Cartviewmodel();
        }
        public int Save()
        {
            return _cartRepository.Save();
        }

        private void UpdateCartTotalPrice(Cart cart)
        {
            if (cart == null || cart.CartItems == null)
                return;

            cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
        }


    }
}
