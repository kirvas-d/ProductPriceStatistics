using Microsoft.EntityFrameworkCore;
using DbModels = ProductPriceStatistics.Infrastructure.EFCoreRepository.Models;
using CoreModels = ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository
{
    class PriceRepository: IPriceRepository
    {
        private readonly ProductPriceStatisticsDbContext _productPriceStatisticsDbContext;

        private DbSet<DbModels.Product> Products => _productPriceStatisticsDbContext.Products;
        private DbSet<DbModels.Store> Stores => _productPriceStatisticsDbContext.Stores;
        private DbSet<DbModels.Price> Prices => _productPriceStatisticsDbContext.Prices;

        public PriceRepository(DbContextOptions<ProductPriceStatisticsDbContext> dbContextOptions)
        {
            _productPriceStatisticsDbContext = new ProductPriceStatisticsDbContext(dbContextOptions);
        }

        public void AddPrice(CoreModels.Price price)
        {
            var product = Products.Where(p => p.GlobalProductId == price.ProductId).FirstOrDefault();
            if (product == null)
            {
                throw new Exception();
            }

            var store = Stores.Where(p => p.Name == price.Store.Name).FirstOrDefault();
            if (store == null)
            {
                throw new Exception();
            }

            Prices.Add(new DbModels.Price()
            {
                ProductId = product.Id,
                StoreId = store.StoreId,
                Value = price.Value,
                DateTimeStamp = price.DateTimeStamp,
            });

            _productPriceStatisticsDbContext.SaveChanges();
        }

        public IEnumerable<CoreModels.Price> GetAllPrices()
        {
            foreach (var price in Prices)
            {
                yield return new CoreModels.Price(price.Product.GlobalProductId,
                                                  price.Value,
                                                  new CoreModels.Store(price.Store.Name),
                                                  price.DateTimeStamp);
            }
        }

        public IEnumerable<CoreModels.Price> GetPricesOfProduct(Guid productId, DateTime? startDateTimeStamp, DateTime? finishDateTimeStamp)
        {
            IEnumerable<DbModels.Price> pricesOfProduct = Prices.Where(p => p.Product.GlobalProductId == productId);

            if (startDateTimeStamp != null)
            {
                pricesOfProduct = pricesOfProduct.Where(p => startDateTimeStamp < p.DateTimeStamp);
            }

            if (finishDateTimeStamp != null)
            {
                pricesOfProduct = pricesOfProduct.Where(p => p.DateTimeStamp < finishDateTimeStamp);
            }

            foreach (var price in pricesOfProduct)
            {
                yield return new CoreModels.Price(price.Product.GlobalProductId,
                                                      price.Value,
                                                      new CoreModels.Store(price.Store.Name),
                                                      price.DateTimeStamp);
            }
        }
    }
}
