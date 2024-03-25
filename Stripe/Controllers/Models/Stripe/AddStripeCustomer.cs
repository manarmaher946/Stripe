namespace Stripe.Controllers.Models.Stripe
{
    public class AddStripeCustomer
    {
        public string Name { get; set; }
        public string  Email { get; set; }
        public string  Token { get; set; }
        public AddStripeCard? stripeCard { get; set; }


    }
}
