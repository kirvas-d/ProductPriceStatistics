using Microsoft.EntityFrameworkCore;
using DbModels = ProductPriceStatistics.Infrastructure.EFCoreRepository.Models;
using CoreModels = ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository
{
    public class PriceRepository: IPriceRepository, IDisposable
    {
        private readonly ProductPriceStatisticsDbContext _context;

        private DbSet<DbModels.Product> Products => _context.Products;
        private DbSet<DbModels.Store> Stores => _context.Stores;
        private DbSet<DbModels.Price> Prices => _context.Prices;

        public PriceRepository(ProductPriceStatisticsDbContext context)
        {
            _context = context;
        }

        public void AddPrice(CoreModels.Price price)
        {
            var product = Products.Where(p => p.GlobalProductId == price.ProductId).FirstOrDefault();
            if (product == null)
            {
                throw new Exception($"Product with id {price.ProductId} was not found");
            }

            var store = Stores.Where(p => p.Name == price.Store.Name).FirstOrDefault();
            if (store == null)
            {   
                store = new DbModels.Store() { Name = price.Store.Name };
                Stores.Add(store);
            }

            Prices.Add(new DbModels.Price()
            {
                Product = product,
                Store = store,
                Value = price.Value,
                DateTimeStamp = price.DateTimeStamp,
            });

            _context.SaveChanges();
        }

        public IEnumerable<CoreModels.Price> GetAllPrices()
        {
            var prices = Prices.Include(p => p.Product)
                .Include(p => p.Store);

            foreach (var price in prices)
            {
                yield return new CoreModels.Price(price.Product.GlobalProductId,
                                                  price.Value,
                                                  new CoreModels.Store(price.Store.Name),
                                                  price.DateTimeStamp);
            }
        }

        public IEnumerable<CoreModels.Price> GetPricesOfProduct(Guid productId, DateTime? startDateTimeStamp, DateTime? finishDateTimeStamp)
        {
            IEnumerable<DbModels.Price> pricesOfProduct = Prices.Where(p => p.Product.GlobalProductId == productId)
                .Include(p => p.Product)
                .Include(p => p.Store);

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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
