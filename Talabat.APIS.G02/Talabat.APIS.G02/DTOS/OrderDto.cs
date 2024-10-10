using Talabat.Core.Order_Aggregate;

namespace Talabat.APIS.G02.DTOS
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto Address { get; set; }
    }
}
