using ProductPriceStatistics.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Domain.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product product);

        Product GetProductById(Guid productId);

        IEnumerable<Product> GetAllProducts();
    }
}
