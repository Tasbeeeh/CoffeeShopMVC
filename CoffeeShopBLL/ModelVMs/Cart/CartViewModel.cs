using CoffeeShopBLL.ModelVMs.CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.ModelVMs.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public string UserId { get; set; } = null!;
        public ICollection<CartItemVM>? CartItems { get; set; }

    }
}