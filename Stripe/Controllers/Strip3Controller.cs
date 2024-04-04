using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Stripe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Strip3Controller : ControllerBase
    {
       public Strip3Controller() { }

        public   IActionResult createProduct()
        {
            ProductService productsService = new ProductService();
            var prodCreateOptions = new ProductCreateOptions
            {
                Name = "pro1",
                Description = "nice",
                //Images = new List<string> { fileLink.Url },
                //Features =
                //   features.Select(f => new ProductFeatureOptions { Name = f }).ToList(),
                //UnitLabel = "hour",
                //Metadata = new Dictionary<string, string> { ["tier.image"] = imageFileName }
            };

            var newHubProduct =  productsService.CreateAsync(prodCreateOptions);
          var   priceService = new PriceService();

           // Create flat price in product
            var priceCreateOptions = new PriceCreateOptions
            {
                Product = newHubProduct.Id.ToString(),
                //Nickname = newHubProduct.Name,
                Currency = "usd",
                Recurring =
                   new PriceRecurringOptions { Interval = "month", UsageType = "licensed" }
            };

            var newProductPrice =  priceService.CreateAsync(priceCreateOptions);

            // Update default price
             productsService.UpdateAsync(newHubProduct.Id.ToString(), new ProductUpdateOptions
            {
                DefaultPrice = newProductPrice.Id.ToString()
            });


        }
    }
}
