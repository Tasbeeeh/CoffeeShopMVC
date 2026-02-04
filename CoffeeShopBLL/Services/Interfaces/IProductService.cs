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
        void Add(ProductVM Obj,string imgPath);
        void Edit(ProductVM Obj, string imgPath);
        void Delete(int id);
        ProducsPerPageVM GetProductsPerPage(int page, int pageSize);
        int Save();
    }
}
