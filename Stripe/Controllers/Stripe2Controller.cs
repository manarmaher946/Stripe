using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;

namespace Stripe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stripe2Controller : ControllerBase
    {
        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                // Create a customer
                // note for hesham  this is the data of the user of my application 
                var customerOptions = new CustomerCreateOptions
                {
                    Email = "amira133@gmail.com",
                    Name = "Amira"
                };

                var customerService = new CustomerService();
                var customer = customerService.Create(customerOptions);

                // Create a Checkout session
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = paymentRequest.Currency,
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = paymentRequest.Description,
                                },
                                UnitAmount = paymentRequest.Amount,
                            },
                            Quantity = 1,
                        },

                    },
                    Mode = "payment",
                    SuccessUrl = "https://yourwebsite.com/success",
                    CancelUrl = "https://yourwebsite.com/cancel",
                    Customer = customer.Id, 
                };

                var service = new SessionService();
                var session = service.Create(options);
                return Ok(new { sessionId = session.Id ,customerName=session.CustomerDetails.Email});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class PaymentRequest
    {
        public string Description { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
    }
}
