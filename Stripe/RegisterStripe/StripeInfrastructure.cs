
using Stripe;
using Stripe.Services;
namespace Stripe_Payments_Web_Api
{
    public static class StripeInfrastructure
    {
        public static IServiceCollection AddStripeInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSettings:SecretKey");

            return services
                .AddScoped<CustomerService>()
                .AddScoped<ChargeService>()
                .AddScoped<TokenService>()
            .AddScoped<Istripeservices, StripeAppServices>();
        }
    }
}

