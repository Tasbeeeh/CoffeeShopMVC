using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Entities
{
    public class Voucher
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;

        public decimal DiscountAmount { get; set; }
        public decimal MinimumCartTotal { get; set; }

        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int UsageLimit { get; set; }
        public int UsedCount { get; set; }

    }
}
