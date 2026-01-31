using CoffeeShopBLL.ModelVMs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Interfaces
{
    public interface ICartService
    {
        IEnumerable<CartViewModel> GetAll();
        CartViewModel GetById(int id);
        void Add(CartViewModel Obj);
        void Edit(CartViewModel Obj);
        void Delete(int id);
        int Save();
    }
}
