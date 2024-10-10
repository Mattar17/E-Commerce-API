using Talabat.Core.Entities;

namespace Talabat.APIS.G02.DTOS
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItem> Item { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }
    }
}
