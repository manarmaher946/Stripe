﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe.Controllers.Models.Stripe;
using Stripe.Services;


namespace Stripe_Payments_Web_Api.Controllers
{
    [Route("api/[controller]")]
    public class StripeController : Controller
    {
        private readonly Istripeservices _stripeService;

        public StripeController(Istripeservices stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("customer/add")]
        public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromBody] AddStripeCustomer customer,CancellationToken ct)
        {
            StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(customer.Token,customer,ct);

            return StatusCode(StatusCodes.Status200OK, createdCustomer);
        }

        [HttpPost("payment/add")]
        public async Task<ActionResult<StripePayment>> AddStripePayment([FromBody] AddStripePayment payment,CancellationToken ct)
        {
        StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(payment,ct);

            return StatusCode(StatusCodes.Status200OK, createdPayment);
        }
    }
}

