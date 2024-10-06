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
    public class OrderConfigurations : IEntityTypeConfiguration<Orders>
        {
        public void Configure(EntityTypeBuilder<Orders> builder)
            {
            builder.Property(order => order.Status)
            .HasConversion(status => status.ToString(), status => (OrderStatus)Enum.Parse(typeof(OrderStatus), status));

            builder.Property(order => order.SubTotal).HasColumnType("decimal(10,2)");

            builder.OwnsOne(order=>order.ShippingAddress,address=>address.WithOwner());
            }
        }
    }
