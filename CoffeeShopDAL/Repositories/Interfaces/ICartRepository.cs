using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Cart? GetCartById(int CartId);
        Cart? GetCartWithItems(string UserId);
        Cart? GetUserCart(string UserId);
        void CreateCart(Cart cart);
        void DeleteCart(int CartId);
        void ClearCart(int CartId);
        Cart? GetCartWithItemsByCartId(int cartId);
        Cart GetLatestCartByUser(string userId);

        int Save();
    }
}