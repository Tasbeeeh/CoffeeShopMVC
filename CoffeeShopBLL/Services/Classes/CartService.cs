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

        public void Add(CartViewModel Obj)
        {
            var cart = new Cart
            {
                TotalPrice = Obj.TotalPrice,
                CreatedAt = Obj.CreatedAt,
            };
            _cartRepository.Add(cart);
        }

        public void Delete(int id)
        {
            _cartRepository.Delete(id);
        }

        public void Edit(CartViewModel Obj)
        {
            var cart = new Cart
            {
                TotalPrice = Obj.TotalPrice,
                CreatedAt = Obj.CreatedAt,
            };
            _cartRepository.Edit(cart);
        }

        public IEnumerable<CartViewModel> GetAll()
        {
            var Carts = _cartRepository.GetAll();
            if (Carts == null || !Carts.Any()) return [];
            var CartssViewModels = Carts.Select(X => new CartViewModel
            {
                TotalPrice = X.TotalPrice,
                CreatedAt = X.CreatedAt,
            });
            return CartssViewModels;
        }

        public CartViewModel GetById(int id)
        {
            Cart cart = _cartRepository.GetById(id);
            return cart.Cartviewmodel();
        }

        public int Save()
        {
            return _cartRepository.Save();
        }
    }
}
