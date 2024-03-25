namespace Stripe.Controllers.Models.Stripe
{
    public class AddStripePayment
    {
         
        public string CustomerId { get; set; }
        public string ReceiptEmail { get; set; }
        public string description { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }
      
    }
}
