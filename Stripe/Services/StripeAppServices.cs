using Stripe.Controllers.Models.Stripe;

namespace Stripe.Services
{
    public class StripeAppServices : Istripeservices
    {
        private readonly CustomerService _customerService;
        private readonly ChargeService _chargeService;

        public StripeAppServices(CustomerService customerService, ChargeService chargeService)
        {
            _customerService = customerService;
            _chargeService = chargeService;
        }

        public async Task<StripeCustomer> AddStripeCustomerAsync(string token, AddStripeCustomer customer, CancellationToken ct)
        {
            // Create a customer using the token
            var options = new CustomerCreateOptions
            {
                Source = token,
                Email = customer.Email,
                Name = customer.Name
            };
            var createdCustomer = await _customerService.CreateAsync(options, cancellationToken: ct);

            // Return the created customer
            return new StripeCustomer(createdCustomer.Name, createdCustomer.Email, createdCustomer.Id);
        }

        public async Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct)
        {
            // Create a charge for the payment
            var options = new ChargeCreateOptions
            {
                Amount = payment.Amount,
                Currency = payment.Currency,
                Customer = payment.CustomerId,
                Description = payment.description,
                ReceiptEmail = payment.ReceiptEmail
            };
            var createdCharge = await _chargeService.CreateAsync(options, cancellationToken: ct);

            // Return the created payment
            return new StripePayment(
                createdCharge.CustomerId,
                createdCharge.ReceiptEmail,
                createdCharge.Description,
                createdCharge.Currency,
                createdCharge.Amount,
                createdCharge.Id);
        }
    }
}

