using CoffeeShopBLL.ModelVMs.Cart;
using CoffeeShopBLL.ModelVMs.CartItem;
using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Mapping
{
    public static class CartMapping
    {
        public static CartViewModel Cartviewmodel(this Cart cart)
        {
            return new CartViewModel
            {
                Id = cart.Id,
                TotalPrice = cart.TotalPrice,
                CreatedAt = cart.CreatedAt,
                UserId = cart.UserId,
                CartItems = cart.CartItems?.Select(ci => new CartItemVM
                {
                    CartId = ci.CartId,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product?.Name ?? "",
                    Quantity = ci.Quantity,
                    UnitPrice = ci.UnitPrice
                }).ToList() ?? new List<CartItemVM>()
            };
        }

    }
}