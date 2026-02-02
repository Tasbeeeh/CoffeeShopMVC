using CoffeeShopDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.ModelVMs.Product
{
    public class ProducsPerPageVM
    {
        public List<ProductVM> Products {  get; set; }
        public int Page {  get; set; }
        public int TotalPages { get; set; }

    }
}
