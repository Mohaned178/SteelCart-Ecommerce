using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : BaseController
    {
        public BasketsController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("get-basket-item/{id}")]
        public async Task<IActionResult> get(string id)
        {
            var res = await work.CustomerBasket.GetBasketAsync(id);
            if (res == null)
            {
                return Ok(new CustomerBasket());
            }
            return Ok(res);
        }
        [HttpPost("update-basket")]
        public async Task<IActionResult> add(CustomerBasket basket)
        {
            var res = await work.CustomerBasket.UpdateBasketAsync(basket);
            return Ok(res);
        }
        [HttpDelete("delete-basket-item/{id}")]
        public async Task<IActionResult> delete(string id)
        {
            var result = await work.CustomerBasket.DeleteBasketAsync(id);
            return result ? Ok(new ResponseAPI(200, "item deleted!")) :
             BadRequest(new ResponseAPI(400));
        }

    }
}
