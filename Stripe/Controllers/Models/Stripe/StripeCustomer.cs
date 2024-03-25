namespace Stripe.Controllers.Models.Stripe
{
    public class StripeCustomer
    {
        public StripeCustomer(string name, string email, string id)
        {
            Name = name;
            Email = email;
            CustomerId = id;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string CustomerId { get; set; }

    }
}
