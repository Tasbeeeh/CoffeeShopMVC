using CoffeeShopBLL.ModelVMs.CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Interfaces
{
    public interface ICartItemService
    {
        List<CartItemVM> GetItems(int cartId);

        void AddItem(int cartId, AddItemVM vm);

        void UpdateQuantity(int cartId, int productId, UpdateQuantityVM vm);

        void RemoveItem(int cartId, int productId);
        int Save();
    }
}
