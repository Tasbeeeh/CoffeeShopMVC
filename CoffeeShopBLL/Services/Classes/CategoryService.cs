using CoffeeShopBLL.Mapping;
using CoffeeShopBLL.ModelVMs.Category;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Classes;
using CoffeeShopDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Classes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository ) {
            _categoryRepository = categoryRepository;
        }
        public void Add(CategoryVM Obj)
        {
            var category = new Category
            {
                Name = Obj.Name,
            };
            _categoryRepository.Add(category);
        }

        public void Delete(int id)
        {

            _categoryRepository.Delete(id);
        }

        public void Edit(CategoryVM Obj)
        {
            var category = new Category { Id = Obj.Id, Name = Obj.Name, };
            _categoryRepository.Edit(category);
        }

        public List<CategoryVM> GetAll()
        {
            return _categoryRepository.GetAll().Select(c => c.categoryVM()).ToList();
        }

        public CategoryVM GetById(int id)
        {
            Category category = _categoryRepository.GetById(id);
            return category.categoryVM();
        }

        public int Save()
        {
            return _categoryRepository.Save();
        }
    }
}
