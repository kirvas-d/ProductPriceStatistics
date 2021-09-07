using ProductPriceStatistics.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Domain.Repositories
{
    public interface IProductStatisticsRepository
    {
        IEnumerable<Product> GetProducts();

        IEnumerable<ProductStatistics> GetProductStatistic(string productName);

        Guid? GetProductIdByName(string productName);

        bool AddProduct(Product product);

        bool AddPriceToProduct(Guid productId, Price price);
    }
}
