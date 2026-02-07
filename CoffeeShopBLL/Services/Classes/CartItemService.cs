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
        private readonly ICartRepository _cartRepository;
        private readonly CoffeeShopDbContext _context;

        public CartItemService(ICartItemRepository cartItemRepository, CoffeeShopDbContext context, ICartRepository cartRepository)
        {
            _cartItemRepository = cartItemRepository;
            _context = context;
            _cartRepository = cartRepository;
        }

        private void UpdateCartTotalPrice(int cartId)
        {
            var cart = _cartRepository.GetCartWithItemsByCartId(cartId);
            if (cart == null) return;

            cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price);
            _cartRepository.Save();
        }

        public void AddItem(int cartId, AddItemVM vm)
        {
            var product = _context.Products.Find(vm.ProductId);

            if (product == null)
                throw new Exception("Product not found");

            if (product.Quantity.HasValue && product.Quantity.Value < vm.Quantity)
                throw new Exception("Not enough stock for this product");

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

            if (product.Quantity.HasValue)
            {
                product.Quantity -= vm.Quantity;
                _context.Products.Update(product);
            }

            _cartItemRepository.Save();
            UpdateCartTotalPrice(cartId);
        }


        public List<CartItemVM> GetItems(int cartId)
        {
            return _cartItemRepository.GetAll(cartId).Select(c=>c.CartItemMap()).ToList();
        }

        public void RemoveItem(int cartId, int productId)
        {
            var item = _cartItemRepository.Get(cartId, productId);

            if (item == null)
                throw new Exception("Cart item not found");

            var product = _context.Products.Find(productId);
            if (product != null && product.Quantity.HasValue)
            {
                product.Quantity += item.Quantity;
                _context.Products.Update(product);
            }

            _cartItemRepository.Delete(item);
            _cartItemRepository.Save();
            UpdateCartTotalPrice(cartId);
        }


        public void UpdateQuantity(int cartId, int productId, UpdateQuantityVM vm)
        {
            var item = _cartItemRepository.Get(cartId, productId);

            if (item == null)
                throw new Exception("Cart item not found");

            var product = _context.Products.Find(productId);

            if (product == null)
                throw new Exception("Product not found");

            int difference = vm.Quantity - item.Quantity;

            if (product.Quantity.HasValue && product.Quantity.Value < difference)
                throw new Exception("Not enough stock to update quantity");

            item.Quantity = vm.Quantity;
            _cartItemRepository.Update(item);

            if (product.Quantity.HasValue)
            {
                product.Quantity -= difference;
                _context.Products.Update(product);
            }

            _cartItemRepository.Save();
            UpdateCartTotalPrice(cartId);
        }

    }
}
