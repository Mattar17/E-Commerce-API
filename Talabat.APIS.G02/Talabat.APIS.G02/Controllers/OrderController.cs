using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIS.G02.DTOS;
using Talabat.APIS.G02.Errors;
using Talabat.Core.Order_Aggregate;
using Talabat.Core.Services;

namespace Talabat.APIS.G02.Controllers
{
    
    public class OrderController : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,IMapper mapper) 
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Orders),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Orders>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto , Address>(orderDto.Address);
            var Order = await _orderService.CreateOrderAsync(buyerEmail , orderDto.BasketId , orderDto.DeliveryMethodId , MappedAddress);

            return Order is null? BadRequest(new ApiResponse(400,"Order Not Created, Error!!")) : Ok(Order);
        }
    }
}
