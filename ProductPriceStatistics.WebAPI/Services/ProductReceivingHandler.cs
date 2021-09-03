using ProductStoreMicroservice.Models;
using ProductWebApi.Data;
using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStoreMicroservice.Services
{
    public class ProductReceivingHandler : IProductReceivingHandler<ProductMeasure>
    {
        private readonly ProductsContext productsContext;

        public ProductReceivingHandler(ProductsContext productsContext) 
        {
            this.productsContext = productsContext;
        }

        public void Handle(ProductMeasure productMeasure)
        {
            Product product = productsContext.Products.FirstOrDefault(p => p.Name == productMeasure.Name);

            if (product == null) 
            {
                productsContext.Products.Add(new Product() { Name = productMeasure.Name });
                productsContext.SaveChanges();
                product = productsContext.Products.FirstOrDefault(p => p.Name == productMeasure.Name);
            }

            Shop shop = productsContext.Shops.FirstOrDefault(s => s.Name == productMeasure.StoreName);

            if (shop == null)
            {
                productsContext.Shops.Add(new Shop() { Name = productMeasure.StoreName });
                productsContext.SaveChanges();
                shop = productsContext.Shops.FirstOrDefault(s => s.Name == productMeasure.StoreName);
            }

            productsContext.Prices.Add(new Price()
            {
                Value = productMeasure.Price,
                DateTimeMeassure = productMeasure.PriceMeasure,
                Product = product,
                Shop = shop
            });

            productsContext.SaveChanges();
        }
    }
}
