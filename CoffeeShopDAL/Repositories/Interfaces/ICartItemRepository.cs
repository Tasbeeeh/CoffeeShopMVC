using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Repositories.Interfaces
{
    public interface ICartItemRepository 
    {
        List<CartItem> GetAll(int cartId);

        CartItem? Get(int cartId, int productId);

        void Add(CartItem item);

        void Update(CartItem item);

        void Delete(CartItem item);

        int Save();
    }
}
