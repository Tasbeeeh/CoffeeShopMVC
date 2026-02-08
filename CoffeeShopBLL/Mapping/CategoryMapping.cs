using CoffeeShopBLL.ModelVMs.Category;
using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Mapping
{
    public static class CategoryMapping
    {
        public static CategoryVM categoryVM(this Category category)
        {
            return new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image
            };
        }
    }
}
