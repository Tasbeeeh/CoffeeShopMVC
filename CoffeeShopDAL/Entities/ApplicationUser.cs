using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoffeeShopDAL.Entities
{
    public class ApplicationUser :IdentityUser
    {
        public Address Address { get; set; } = null!; 
        
        //relations
        public Cart Cart { get; set; } = null!;

        public ICollection<Order>? Orders { get; set; }

    }
    [Owned]
    public class Address
    {
        public int BuildingNumber { get; set; }
        public string City { get; set; }=null!;
        public string Street { get; set; } = null!;
    }
}
