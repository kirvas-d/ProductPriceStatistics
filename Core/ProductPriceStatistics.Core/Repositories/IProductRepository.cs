using ProductPriceStatistics.Core.Models;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product product);

        Product GetProductById(Guid productId);

        IEnumerable<Product> GetAllProducts();
    }
}
