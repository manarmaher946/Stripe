using Stripe.Controllers.Models.Stripe;
using Stripe.Issuing;

namespace Stripe.Services
{
    public class StripeAppServices : Istripeservices
    {
        private readonly CustomerService _customerService;
        private readonly PaymentMethodService _paymentMethodService;
        private readonly PaymentIntentService _paymentIntentService;

        public StripeAppServices(CustomerService customerService, PaymentIntentService PaymentIntentService, PaymentMethodService paymentMethodService)
        {
            _customerService = customerService;
            _paymentIntentService = PaymentIntentService;
            _paymentMethodService = paymentMethodService;
        }

        //public async Task<StripeCustomer> AddStripeCustomerAsync(AddStripeCustomer customer, CancellationToken ct)
        //{
        //    // Set Stripe Token options based on customer data
        //    TokenCreateOptions tokenOptions = new TokenCreateOptions
        //    {
        //        Card = new TokenCardOptions
        //        {
        //            Name = customer.Name,
        //            Number = customer.CreditCard.CardNumber,
        //            ExpYear = customer.CreditCard.ExpirationYear,
        //            ExpMonth = customer.CreditCard.ExpirationMonth,
        //            Cvc = customer.CreditCard.Cvc
        //        }
        //    };

        //    // Create new Stripe Token
        //    Token stripeToken = await _tokenService.CreateAsync(tokenOptions, null, ct);

        //    // Set Customer options using
        //    CustomerCreateOptions customerOptions = new CustomerCreateOptions
        //    {
        //        Name = customer.Name,
        //        Email = customer.Email,
        //        Source = stripeToken.Id
        //    };

        //    // Create customer at Stripe
        //    Customer createdCustomer = await _customerService.CreateAsync(customerOptions, null, ct);

        //    // Return the created customer at stripe
        //    return new StripeCustomer(createdCustomer.Name, createdCustomer.Email, createdCustomer.Id);
        //}


        public async Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct)
        {
            var paymentMethodOptions = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Token = payment.stripeCard.Token
                },
               
            };

            // Create a PaymentMethod
            var paymentMethod = await _paymentMethodService.CreateAsync(paymentMethodOptions, cancellationToken: ct);

            // Attach the PaymentMethod to a Customer
            var customerOptions = new CustomerCreateOptions
            {
                Email = "MANARMAHER@GMAIL.COM",
                PaymentMethod = paymentMethod.Id,
            };
            var customer = await _customerService.CreateAsync(customerOptions, cancellationToken: ct);

            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                Amount = payment.Amount,
                Currency = payment.Currency,
                Customer = customer.Id,
                PaymentMethod = paymentMethod.Id,
                Confirm = true,
                ConfirmationMethod = "manual",
                ReturnUrl = "https://example.com/return-url",
            };
            var paymentIntent = await _paymentIntentService.CreateAsync(paymentIntentOptions, cancellationToken: ct);


            // Return the created payment
            return new StripePayment(
                paymentIntent.CustomerId,
                paymentIntent.ReceiptEmail,
                paymentIntent.Description,
                paymentIntent.Currency,
                paymentIntent.Amount,
                paymentIntent.Id);
        }
    }
}    


