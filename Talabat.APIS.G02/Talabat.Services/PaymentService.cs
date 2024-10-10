using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IBasketRepository basketRepository,IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeKeys:SecretKey"];
            var Basket = await _basketRepository.GetBasketAsync(BasketId);

            if (Basket == null)
                return null;

           // Get Shipping cost
           var ShippingCost = 0M;
           if (Basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(Basket.DeliveryMethodId.Value);
                ShippingCost = deliveryMethod.Cost;
            }

           //Assign Product Prices to Basket Items Prices
           if (Basket.Item.Count > 0)
            {
                foreach (var item in Basket.Item)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if (item.Price != product.Price)
                        item.Price = product.Price;
                }
            }

            var SubTotal = Basket.Item.Sum(item => item.Price * item.Quantity);

            //Payment Intent service
            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(Basket.PaymentIntentId))
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Amount = (long) (SubTotal*100 + ShippingCost*100)
                };
                paymentIntent = await paymentIntentService.CreateAsync(Options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)( SubTotal * 100 + ShippingCost * 100 )
                };
                paymentIntent = await paymentIntentService.UpdateAsync(Basket.PaymentIntentId , Options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }

            await _basketRepository.UpdateBasketAsync(Basket);
            return Basket;
           
        }
    }
}
