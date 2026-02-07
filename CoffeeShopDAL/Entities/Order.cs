using CoffeeShopDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }

        //relations

        public ApplicationUser User { get; set; } = null!;
        public string UserId { get; set; } = null!;

       
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
