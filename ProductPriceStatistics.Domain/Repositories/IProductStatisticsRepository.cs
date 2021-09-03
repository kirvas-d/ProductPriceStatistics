using ProductPriceStatistics.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Domain.Repositories
{
    interface IProductStatisticsRepository
    {
        IEnumerable<Product> GetProducts();

        IEnumerable<ProductStatistics> GetProductStatistic(string productName);

        bool AddPriceToProduct(Guid productId, Price price);
    }
}
