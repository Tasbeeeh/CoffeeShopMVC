using CoffeeShopBLL.ModelVMs.Product;
using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Mapping
{
    public static class ProductMapping
    {
        public static ProductVM ProductMap(this Product product)
        {
            return new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                //InStock = product.InStock,
                Price = product.Price,
                ProductSize = product.ProductSize,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category != null ? product.Category.Name : null
            };
        }
    }
}
