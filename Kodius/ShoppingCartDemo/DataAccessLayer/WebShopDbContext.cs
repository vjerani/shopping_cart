using Microsoft.EntityFrameworkCore;
using ShoppingCartDemo.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer
{
    public class WebShopDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PromotionCode> PromotionCodes { get; set; }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options)
        : base(options)
            {
            }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Configuration.OrderConfiguration());
            modelBuilder.ApplyConfiguration(new Configuration.OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new Configuration.ProductConfiguration());
            modelBuilder.ApplyConfiguration(new Configuration.PromotionCodeConfiguration());
            modelBuilder.Entity<Product>()
            .HasData(
                new Product
                {
                    Id = 1,
                    Name = "Smart Hub",
                    Price = 49.99F
                },
                new Product
                {
                    Id = 2,
                    Name = "Motion Sensor",
                    Price = 24.99F
                },
                new Product
                {
                    Id = 3,
                    Name = "Wireless Camera",
                    Price = 99.99F
                },
                new Product
                {
                    Id = 4,
                    Name = "Smoke Sensor",
                    Price = 19.99F
                },
                new Product
                {
                    Id = 5,
                    Name = "Water Leak Sensor",
                    Price = 14.99F
                }
            );

            modelBuilder.Entity<PromotionCode>()
                .HasData(
                    new PromotionCode
                    {
                        Id = 1,
                        IsDiscount = true,
                        Name = "20%OFF",
                        UseInConjuction = false,
                        Amount = 0.2F
                    },
                    new PromotionCode
                    {
                        Id = 2,
                        IsDiscount = true,
                        Name = "5%OFF",
                        UseInConjuction = true,
                        Amount = 0.05F
                    },
                    new PromotionCode
                    {
                        Id = 3,
                        IsDiscount = false,
                        Name = "20EUROFF",
                        UseInConjuction = true,
                        Amount = 20F
                    }
                );
        }
    }
}
