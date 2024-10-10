using Talabat.Core.Order_Aggregate;

namespace Talabat.APIS.G02.DTOS
{
    public class OrderItemDTO
    {
        public ProductItemOrdered Product {get; set;}   
        public int Quantity {get; set;}
        public decimal Price {get; set;}
    }
}
