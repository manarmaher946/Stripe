using Stripe.Controllers.Models.Stripe;

namespace Stripe.Services
{
    public interface Istripeservices
    {
        //Task<StripeCustomer> AddStripeCustomerAsync(string token, AddStripeCustomer customer, CancellationToken ct);
        Task<StripePayment> AddStripePaymentAsync(AddStripePayment payment, CancellationToken ct);
       

    }
}
