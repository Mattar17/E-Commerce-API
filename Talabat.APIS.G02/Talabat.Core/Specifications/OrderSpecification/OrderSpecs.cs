using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregate;

namespace Talabat.Core.Specifications.OrderSpecification
{
    public class OrderSpecs : BaseSpecification<Orders>
    {
        public OrderSpecs(string email): base(order=>order.BuyerEmail == email)
        {
            Includes.Add(order => order.DeliveryMethod);
            Includes.Add(order => order.Items);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderSpecs(string email,int OrderId) : base(order => order.BuyerEmail == email && order.Id == OrderId)
        {
            Includes.Add(order => order.DeliveryMethod);
            Includes.Add(order => order.Items);
        }
    }
}
