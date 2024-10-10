using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.G02.DTOS;
using Talabat.APIS.G02.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Services;

namespace Talabat.APIS.G02.Controllers
{
   
    public class PaymentController : APIBaseController
    {
        private readonly IPaymentService paymentService;
        private readonly IMapper mapper;

        public PaymentController(IPaymentService paymentService,IMapper mapper)
        {
            this.paymentService = paymentService;
            this.mapper = mapper;
        }
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CustomerBasketDTO) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var customerBasket = await paymentService.CreateOrUpdatePaymentIntent(BasketId);

            if (customerBasket == null)
                return BadRequest(new ApiResponse(400 , "There Was a problem in Payment"));

            var MappedBasket = mapper.Map<CustomerBasket , CustomerBasketDTO>(customerBasket);
            return Ok(MappedBasket);
        }
    }
}
