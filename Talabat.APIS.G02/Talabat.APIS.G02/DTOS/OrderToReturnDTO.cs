using Talabat.Core.Order_Aggregate;

namespace Talabat.APIS.G02.DTOS
{
    public class OrderToReturnDTO
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public ICollection<OrderItemDTO> Items { get; set; } = new HashSet<OrderItemDTO>();
        public Address ShippingAddress { get; set; }
        public string Status { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public int SubTotal { get; set; }
        public int Total { get; set; }
        public string PaymentIntendId { get; set; }
    }
}
