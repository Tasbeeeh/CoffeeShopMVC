using CoffeeShopBLL.ModelVMs.Cart;
using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Interfaces
{
    public interface ICartService
    {
        CartViewModel? GetCartById(int cartId);
        CartViewModel? GetCartWithItems(string userId);
        CartViewModel CreateCart(string userId);
        CartViewModel GetUserCart(string userId);
        bool ClearCart(int cartId);
        bool DeleteCart(int cartId);
    }
}