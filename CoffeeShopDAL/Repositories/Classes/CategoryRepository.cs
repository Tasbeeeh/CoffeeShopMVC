using CoffeeShopDAL.Data;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Repositories.Classes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CoffeeShopDbContext _context;

        public CategoryRepository(CoffeeShopDbContext context) {
              _context = context;
        }
        public void Add(Category Obj)
        {
            _context.Categories.Add(Obj);
        }

        public void Delete(int id)
        {
            Category category = GetById(id);
            _context.Categories.Remove(category);
        }

        public void Edit(Category Obj)
        {
            Category category = GetById(Obj.Id);
            if (category != null) { 
               category.Name = Obj.Name;
            }
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
