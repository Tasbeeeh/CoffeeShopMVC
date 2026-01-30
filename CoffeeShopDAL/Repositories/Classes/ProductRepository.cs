using CoffeeShopDAL.Data;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Repositories.Classes
{
    public class ProductRepository : IProductRepository
    {
        private readonly CoffeeShopDbContext _context;

        public ProductRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Delete(int id)
        {
            _context.Products.Remove(GetById(id));
        }

        public void Edit(Product product)
        {
            _context.Products.Update(product);
        }

        public List<Product> GetAll()
        {
            return _context.Products.Include(p => p.Category).ToList();
        }
       

        public Product GetById(int id)
        {
            return _context.Products.Find(id)!;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }





        public List<Product> GetProductsByCategory(string categoryName)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.Name.ToLower() == categoryName.ToLower());

            if (category == null)
                return new List<Product>();

            return _context.Products
                .Where(p => p.CategoryId == category.Id)
                .ToList();
        }


        public List<Product> Search(string term)
        {
            return _context.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{term}%"))
                .ToList();
        }

    }
}
