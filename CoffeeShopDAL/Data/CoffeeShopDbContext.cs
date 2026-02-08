using CoffeeShopDAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopDAL.Data
{
    public class CoffeeShopDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Cart>  Carts { get; set; }
        public DbSet<CartItem>  CartItems { get; set; }
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Order>  Orders { get; set; }
        public DbSet<Product>  Products { get; set; }
        public DbSet<Voucher>  Vouchers { get; set; }
        public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options) : base(options)
        {
            
        }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Product>()
                    .Ignore(p => p.InStock);

            builder.Entity<Product>()
                .HasMany(o=>o.CartItems)
                .WithOne(o=>o.Product)
                .HasForeignKey(o=>o.ProductId); 


            builder.Entity<Cart>()
                .HasMany(o=>o.CartItems)
                .WithOne(o=>o.Cart)
                .HasForeignKey(o=>o.CartId);


            builder.Entity<CartItem>()
                .HasKey(c => new {c.ProductId, c.CartId});

            builder.Entity<ApplicationUser>().ToTable(Tb =>
            {
                Tb.HasCheckConstraint("EmailCheck", "Email Like '_%@_%._%'");
                Tb.HasCheckConstraint("PhoneCheck", "PhoneNumber LIKE '01%' AND PhoneNumber NOT LIKE '%[^0-9]%'");
            });

            builder.Entity<Order>()
                .Property(o => o.OrderDate)
                .HasDefaultValueSql("GETDATE()");

             builder.Entity<Cart>()
                .Property(c=>c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            
           



        }

    }
}
