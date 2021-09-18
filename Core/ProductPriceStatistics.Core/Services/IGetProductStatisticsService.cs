using ProductPriceStatistics.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Domain.Services
{
    public interface IGetProductStatisticsService
    {
        public IEnumerable<Product> GetAllProducts();

        public IEnumerable<Tag> GetAllTags();

        public ProductStatistics GetProductStatistics(Guid productId);
    }
}
