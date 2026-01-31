using CoffeeShopBLL.Mapping;
using CoffeeShopBLL.ModelVMs.CartItem;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Data;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Classes
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly CoffeeShopDbContext _context;

        public CartItemService(ICartItemRepository cartItemRepository, CoffeeShopDbContext context)
        {
            _cartItemRepository = cartItemRepository;
            _context = context;
        }

        public void AddItem(int cartId, AddItemVM vm)
        {
            var product = _context.Products.Find(vm.ProductId);

            if (product == null)
                throw new Exception("Product not found");

            var existingItem = _cartItemRepository.Get(cartId, vm.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += vm.Quantity;
                _cartItemRepository.Update(existingItem);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = vm.ProductId,
                    Quantity = vm.Quantity,
                    UnitPrice = product.Price
                };

                _cartItemRepository.Add(newItem);
            }
        }

        public List<CartItemVM> GetItems(int cartId)
        {
            return _cartItemRepository.GetAll(cartId).Select(c=>c.CartItemMap()).ToList();
        }

        public void RemoveItem(int cartId, int productId)
        {
            var item =  _cartItemRepository.Get(cartId, productId);

            if (item == null)
                throw new Exception("Cart item not found");

            _cartItemRepository.Delete(item);
        }

        public int Save()
        {
           return _cartItemRepository.Save();
        }

        public void UpdateQuantity(int cartId, int productId, UpdateQuantityVM vm)
        {
            var item =  _cartItemRepository.Get(cartId, productId);

            if (item == null)
                throw new Exception("Cart item not found");

            item.Quantity = vm.Quantity;

            _cartItemRepository.Update(item);
        }
    }
}
