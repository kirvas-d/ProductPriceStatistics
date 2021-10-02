using Microsoft.EntityFrameworkCore;
using ProductPriceStatistics.Core.DTOs;
using ProductPriceStatistics.Core.ReadRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using DbModels = ProductPriceStatistics.Infrastructure.EFCoreRepository.Models;

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository
{
    class PriceReadRepository : IPriceReadRepository, IDisposable
    {
        private readonly ProductPriceStatisticsDbContext _context;
        private DbSet<DbModels.Price> Prices => _context.Prices;

        public PriceReadRepository(ProductPriceStatisticsDbContext context)
        {
            if (context == null) throw new ArgumentNullException($"Argument {nameof(context)} is null");

            _context = context;
        }

        public IEnumerable<PriceDto> GetPricesOfProduct(Guid productId, DateTime? startDateTimeStamp, DateTime? finishDateTimeStamp)
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
                yield return new PriceDto(price.Value,
                                          price.Store.Name,
                                          price.DateTimeStamp);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
