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
            return _context.Products.ToList();
        }
       

        public Product GetById(int id)
        {
            return _context.Products.Find(id)!;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
