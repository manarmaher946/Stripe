using System.Runtime.CompilerServices;

namespace Stripe.Controllers.Models.Stripe
{
    public class StripePayment
    {
        public StripePayment(string customerId, string receiptEmail, string description, string currency, long amount, string id)
        {
            CustomerId = customerId;
            ReceiptEmail = receiptEmail;
            this.description = description;
            Currency = currency;
            Amount = amount;
            PaymentId = id;
        }

        public string CustomerId { get; set; }
        public string ReceiptEmail { get; set; }
        public string description { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }
        public string PaymentId { get; set; }
    }
}
