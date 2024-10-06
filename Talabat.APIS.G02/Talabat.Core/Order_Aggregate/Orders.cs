using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Order_Aggregate
{
    public class Orders : BaseEntity
    {

        public Orders(string buyerEmail,ICollection<OrderItem> items,Address shippingAddress,OrderStatus status,DeliveryMethod deliveryMethod,int subTotal) 
        { 
            BuyerEmail = buyerEmail;
            Items = items;
            ShippingAddress = shippingAddress;
            Status = status;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
        }
        public Orders()
        {
            
        }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public Address ShippingAddress { get; set; }
        public OrderStatus Status { get; set; }
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int SubTotal { get; set; }
        public string PaymentIntendId {get;set; }                                                                        
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
    }
}
