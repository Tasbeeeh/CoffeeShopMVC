using CoffeeShopBLL.ModelVMs.Category;
using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryVM> GetAll();
        CategoryVM GetById(int id);
        void Add(CategoryVM Obj);
        void Edit(CategoryVM Obj);
        void Delete(int id);
        int Save();
    }
}
