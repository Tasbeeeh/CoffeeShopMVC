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
        public bool IsValid { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Value { get; set; } = null!;
        public string Code { get; set; }=null!;

        //relatons
        public Cart Cart { get; set; } = null!;
        public int CartId { get; set; }


    }
}
