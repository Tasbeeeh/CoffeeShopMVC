using CoffeeShopBLL.ModelVMs.Cart;
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
                TotalPrice = cart.TotalPrice,
                CreatedAt = cart.CreatedAt,
            };
        }
    }
}
