using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Talabat.APIS.G02.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository;

namespace Talabat.APIS.G02.Controllers
{

    public class BasketController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string id)
        {
            var Basket = await _basketRepository.GetBasketAsync(id);
            return Basket is null ? new CustomerBasket(id) : Basket;
        }

        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var Basket = await _basketRepository.UpdateBasketAsync(basket);

            if (Basket is null)
            {
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest));
            }

            else
            {
                return Ok(Basket);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string Id)
        {
            return await _basketRepository.DeleteBasketAsync(Id);
        }
    }
}
