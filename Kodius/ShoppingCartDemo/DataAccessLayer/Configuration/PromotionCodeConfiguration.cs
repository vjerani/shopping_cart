using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCartDemo.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartDemo.DataAccessLayer.Configuration
{
    public class PromotionCodeConfiguration : IEntityTypeConfiguration<PromotionCode>
    {
        public void Configure(EntityTypeBuilder<PromotionCode> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);
            builder.Property(x => x.IsDiscount).IsRequired(true);
            builder.Property(x => x.UseInConjuction).IsRequired(true);
            builder.Property(x => x.Amount).IsRequired(true);
        }
    }
}
