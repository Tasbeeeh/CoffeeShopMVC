using CoffeeShopBLL.ModelVMs.CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.ModelVMs.Order
{
    public class OrderDetailsVM
    {
     
            public int OrderId { get; set; }
            public DateTime CreatedAt { get; set; }
            public decimal TotalPrice { get; set; }

            public List<CartItemVM> Items { get; set; }
        

    }
}
