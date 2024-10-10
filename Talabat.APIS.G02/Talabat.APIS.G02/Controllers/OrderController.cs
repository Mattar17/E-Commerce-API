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
        [ProducesResponseType(typeof(OrderToReturnDTO) ,StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto , Address>(orderDto.Address);
            var Order = await _orderService.CreateOrderAsync(buyerEmail , orderDto.BasketId , orderDto.DeliveryMethodId , MappedAddress);

            if(Order is null)
                BadRequest(new ApiResponse(400 , "Order Not Created, Error!!"));
            var MappedOrder = _mapper.Map<Orders , OrderToReturnDTO>(Order);
            return Ok(MappedOrder);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDTO>) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersOfUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _orderService.GetOrdersForUserAsync(userEmail);

            if(Orders is null) 
                BadRequest(new ApiResponse(404,"Order Not Found"));

            var MappedOrders = _mapper.Map<IReadOnlyList<Orders> , IReadOnlyList<OrderToReturnDTO>>(Orders);
            return Ok(MappedOrders);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(OrderToReturnDTO) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderbyId(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderService.GetOrderByIdAsync(buyerEmail , id);
            if (Order is null) 
                BadRequest(new ApiResponse(404,"Order Not Found"));

            var MappedOrder = _mapper.Map<Orders, OrderToReturnDTO>(Order);
            return Ok(MappedOrder);
        }

        [HttpGet("DeliveryMethods")]
        [Authorize]

        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var DeliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }
    }
}
