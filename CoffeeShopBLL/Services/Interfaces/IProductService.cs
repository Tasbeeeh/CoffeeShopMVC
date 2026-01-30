using CoffeeShopBLL.ModelVMs.Category;
using CoffeeShopBLL.ModelVMs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductVM> GetAll();
        ProductVM GetById(int id);
        void Add(ProductVM Obj);
        void Edit(ProductVM Obj);
        void Delete(int id);
        int Save();
        List<ProductVM> Search(string term);
        List<ProductVM> GetProductsByCategoryName(string categoryName);
    }
}
