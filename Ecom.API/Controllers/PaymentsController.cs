using Ecom.Core.Entities;
using Ecom.Core.Entities.Order;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.IO;
using System.Threading.Tasks;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<CustomerBasket>> create(string basketId, int? deliveryId)
        {
            return await paymentService.CreateOrUpdatePaymentAsync(basketId, deliveryId);
        }

        const string endpointSecret = "whsec_28cc3dec50be3eaba23c0d5217e31f075148d84948bb1e7aa84452952a3a9461";

        [HttpPost("webhook")]
        public async Task<IActionResult> UpdateStatusWithStripe()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    endpointSecret,
                    throwOnApiVersionMismatch: false
                );

                PaymentIntent intent;
                Orders orders;

                // استخدم النصوص بدل Events.*
                if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    intent = stripeEvent.Data.Object as PaymentIntent;
                    orders = await paymentService.UpdateOrderFaild(intent.Id);
                }
                else if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    intent = stripeEvent.Data.Object as PaymentIntent;
                    orders = await paymentService.UpdateOrderSuccess(intent.Id);
                }
                else
                {
                    Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                }

                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest();
            }
        }
    }
}
