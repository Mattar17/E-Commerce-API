using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Address = Talabat.Core.Order_Aggregate.Address;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork) 
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Orders?> CreateOrderAsync(string buyerEmail , string BasketId , int DeliveryMethodId , Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);
            var OrderItems = new List<OrderItem>();
            
            if (basket?.Item?.Count > 0)
            {
                foreach (var item in basket.Item)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var ProductItemOrdered = new ProductItemOrdered(item.Id,item.Name,item.PictureUrl);
                    OrderItems.Add(new OrderItem(ProductItemOrdered,item.Price,item.Quantity));
                }
            }

            var SubTotal = (int)OrderItems.Sum(item=>item.Price*item.Quantity);
            var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

            var Order = new Orders(buyerEmail,OrderItems,shippingAddress,DeliveryMethod,SubTotal);

            await _unitOfWork.Repository<Orders>().Add(Order);
            var result = await _unitOfWork.CompleteAsync();

            return result>0? Order : null;
        }

        public Task<Orders> GetOrderByIdAsync(string buyerEmail , int OrderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Orders>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
