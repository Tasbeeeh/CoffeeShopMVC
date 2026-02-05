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
    public class CartRepository : ICartRepository
    {
        private readonly CoffeeShopDbContext _context;

        public CartRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }
        public void Add(Cart Obj)
        {
            _context.Carts.Add(Obj);
        }

        public void Delete(int id)
        {
            _context.Carts.Remove(GetById(id));
        }

        public void Edit(Cart Obj)
        {
            _context.Carts.Update(Obj);
        }

        public List<Cart> GetAll()
        {
            return _context.Carts.ToList();
        }

        public Cart GetById(int id)
        {
            return _context.Carts.Find(id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}

