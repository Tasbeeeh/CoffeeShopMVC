using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        //relations
        public Voucher? Voucher { get; set; }


        public ApplicationUser User { get; set; } = null!;
        [ForeignKey("User")]
        public string UserId { get; set; } = null!;

        public ICollection<CartItem> CartItems { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }

    }
}
