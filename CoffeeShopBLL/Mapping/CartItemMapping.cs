using CoffeeShopBLL.ModelVMs.CartItem;
using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Mapping
{
    public static class CartItemMapping
    {
        public static CartItemVM CartItemMap(this CartItem cartItem)
        {
            return new CartItemVM()
            {
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.UnitPrice,
                ProductName= cartItem.Product.Name
            };
        }

    }
}
