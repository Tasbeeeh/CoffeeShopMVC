using CoffeeShopDAL.Data;
using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Repositories.Classes
{
    public class VoucherRepository : IVoucherRepositpry
    {
        private readonly CoffeeShopDbContext _context;

        public VoucherRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public Voucher? GetByCode(string code)
        {
            return _context.Vouchers.FirstOrDefault(v=>v.Code == code);
        }
    }
}
