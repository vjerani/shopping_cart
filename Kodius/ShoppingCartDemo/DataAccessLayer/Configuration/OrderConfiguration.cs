using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCartDemo.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OffFinalCost).IsRequired(true).HasDefaultValue(0);
            builder.Property(x => x.Discount).IsRequired(true).HasDefaultValue(0);
            builder.Property(x => x.OrderTotal).IsRequired(true).HasDefaultValue(0);
            builder.HasMany(x => x.OrderItems).WithOne().HasForeignKey(x => x.OrderId);
        }
    }
}
