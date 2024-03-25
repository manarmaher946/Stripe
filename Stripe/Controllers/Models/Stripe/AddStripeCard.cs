namespace Stripe.Controllers.Models.Stripe
{
    public class AddStripeCard
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
       public string ExpirationYear { get; set; }
       public string Expirationmonth { get; set; }
       public string Cvc { get; set; }
    }
}
