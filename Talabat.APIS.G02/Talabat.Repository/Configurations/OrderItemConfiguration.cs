using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Repository.Configurations
    {
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>

        {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
            {
            builder.Property(item => item.Price).HasColumnType("decimal(18,2)");
            builder.OwnsOne(item=>item.Product,product=>product.WithOwner());
            }
        }
    }
