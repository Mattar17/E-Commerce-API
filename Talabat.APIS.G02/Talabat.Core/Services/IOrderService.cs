using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Entities;

using Address = Talabat.Core.Order_Aggregate.Address;

namespace Talabat.Core.Services
{
    public interface IOrderService
    {
        public Task<Orders?> CreateOrderAsync(string buyerEmail , string BasketId , int DeliveryMethodId , Address shippingAddress);
        public Task<IReadOnlyList<Orders?>> GetOrdersForUserAsync(string buyerEmail);
        public Task<Orders> GetOrderByIdAsync(string buyerEmail,int OrderId);

    }
}
