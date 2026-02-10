using CoffeeShopDAL.Entities;
using CoffeeShopDAL.Entities.Enums;
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



            builder.Entity<Category>().HasData(
            new Category { Id = 7, Name = "Coffee", Image = "coffee.jpg" },
            new Category { Id = 8, Name = "Tea", Image = "tea.jpg" },
            new Category { Id = 9, Name = "Dessert", Image = "dessert.jpg" },
            new Category { Id = 10, Name = "Bakery", Image = "bakery.jpg" },
            new Category { Id = 11, Name = "Drinks", Image = "drinks.jpg" }
            );

            builder.Entity<Product>().HasData(
             // ===== Coffee =====
             new Product { Id = 20, Name = "Espresso", Description = "Strong black coffee", Image = "espresso.jpg", Price = 50, Quantity = null, ProductSize = ProductSize.Small, CategoryId = 7 },
             new Product { Id = 21, Name = "Latte", Description = "Coffee with milk", Image = "latte.jpg", Price = 120, Quantity = null, ProductSize = ProductSize.Medium, CategoryId = 7 },
             new Product { Id = 22, Name = "Cappuccino", Description = "Coffee with foam", Image = "cappuccino.jpg", Price = 130, Quantity = null, ProductSize = ProductSize.Medium, CategoryId = 7 },
             new Product { Id = 23, Name = "Americano", Description = "Diluted espresso", Image = "americano.jpg", Price = 90, Quantity = null, ProductSize = ProductSize.Small, CategoryId = 7 },
             new Product { Id = 24, Name = "Mocha", Description = "Chocolate coffee", Image = "mocha.jpg", Price = 100, Quantity = null, ProductSize = ProductSize.Large, CategoryId = 7 },

             // ===== Tea =====
             new Product { Id = 25, Name = "Green Tea", Description = "Healthy green tea", Image = "green_tea.jpg", Price = 50, Quantity = null, ProductSize = ProductSize.Small, CategoryId = 8 },
             new Product { Id = 26, Name = "Black Tea", Description = "Classic black tea", Image = "black_tea.jpg", Price = 30, Quantity = null, ProductSize = ProductSize.Medium, CategoryId = 8 },
             new Product { Id = 27, Name = "Chamomile Tea", Description = "Relaxing herbal tea", Image = "chamomile_tea.jpg", Price = 60, Quantity = null, ProductSize = ProductSize.Medium, CategoryId = 8 },
             new Product { Id = 28, Name = "Mint Tea", Description = "Refreshing mint tea", Image = "mint_tea.jpg", Price = 40, Quantity = null, ProductSize = ProductSize.Small, CategoryId = 8 },
             new Product { Id = 29, Name = "Earl Grey", Description = "Flavored black tea", Image = "earl_grey.jpg", Price = 70, Quantity = null, ProductSize = ProductSize.Large, CategoryId = 8 },

             // ===== Dessert =====
             new Product { Id = 30, Name = "Cheesecake", Description = "Creamy cheesecake", Image = "cheesecake.jpg", Price = 250, Quantity = 100, ProductSize = ProductSize.Medium, CategoryId = 9 },
             new Product { Id = 31, Name = "Chocolate Brownie", Description = "Rich chocolate brownie", Image = "brownie.jpg", Price = 150, Quantity = 20, ProductSize = ProductSize.Small, CategoryId = 9 },
             new Product { Id = 32, Name = "Macaron", Description = "French macaron", Image = "macaron.jpg", Price = 120, Quantity = 300, ProductSize = ProductSize.Small, CategoryId = 9 },
             new Product { Id = 33, Name = "Tiramisu", Description = "Italian dessert", Image = "tiramisu.jpg", Price = 280, Quantity = 80, ProductSize = ProductSize.Large, CategoryId = 9 },
             new Product { Id = 34, Name = "Cupcake", Description = "Vanilla cupcake", Image = "cupcake.jpg", Price = 100, Quantity = 250, ProductSize = ProductSize.Small, CategoryId = 9 },

             // ===== Bakery =====
             new Product { Id = 35, Name = "Baguette", Description = "French bread", Image = "baguette.jpg", Price = 50, Quantity = 500, ProductSize = ProductSize.Large, CategoryId = 10 },
             new Product { Id = 36, Name = "Croissant", Description = "Buttery croissant", Image = "croissant.jpg", Price = 80, Quantity = 300, ProductSize = ProductSize.Medium, CategoryId = 10 },
             new Product { Id = 37, Name = "Donut", Description = "Sweet donut", Image = "donut.jpg", Price = 150, Quantity = 400, ProductSize = ProductSize.Small, CategoryId = 10 },
             new Product { Id = 38, Name = "Muffin", Description = "Blueberry muffin", Image = "muffin.jpg", Price = 70, Quantity = 200, ProductSize = ProductSize.Small, CategoryId = 10 },
             new Product { Id = 39, Name = "Bread Roll", Description = "Soft bread roll", Image = "bread_roll.jpg", Price = 40, Quantity = 350, ProductSize = ProductSize.Medium, CategoryId = 10 },

             // ===== Drinks =====
             new Product { Id = 40, Name = "Orange Juice", Description = "Freshly squeezed", Image = "orange_juice.jpg", Price = 100, Quantity = null, ProductSize = ProductSize.Medium, CategoryId = 11 },
             new Product { Id = 41, Name = "Lemonade", Description = "Refreshing lemonade", Image = "lemonade.jpg", Price = 80, Quantity = null, ProductSize = ProductSize.Medium, CategoryId = 11 },
             new Product { Id = 42, Name = "Coca Cola", Description = "Sting", Image = "sting.jpg", Price = 60, Quantity = null, ProductSize = ProductSize.Large, CategoryId = 11 },
             new Product { Id = 43, Name = "Fanta", Description = "Orange soda", Image = "fanta.jpg", Price = 60, Quantity = null, ProductSize = ProductSize.Large, CategoryId = 11 },
             new Product { Id = 44, Name = "Mineral Water", Description = "Kiwi Juice", Image = "kiwi_juice.jpg", Price = 150, Quantity = null, ProductSize = ProductSize.Small, CategoryId = 11 }
             );




        }

    }
}
