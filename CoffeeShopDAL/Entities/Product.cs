using CoffeeShopDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image { get; set; }

        public ProductSize ProductSize { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public bool InStock => Quantity != 0;

        //relations
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }

        public ICollection<CartItem> CartItems { get; set; } =new List<CartItem>();
        public ICollection<OrderItem>? OrderItems { get; set; }

    }
}
