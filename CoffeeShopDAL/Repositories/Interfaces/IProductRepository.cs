using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> Search(string term);
        List<Product> GetProductsByCategory(string categoryName);
    }
}
