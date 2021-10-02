using Microsoft.EntityFrameworkCore;
using ProductPriceStatistics.Core.DTOs;
using ProductPriceStatistics.Core.ReadRepositories;
using System;
using System.Collections.Generic;
using DbModels = ProductPriceStatistics.Infrastructure.EFCoreRepository.Models;

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository
{
    class ProductReadRepository : IProductReadRepository
    {
        private readonly ProductPriceStatisticsDbContext _context;

        private DbSet<DbModels.Product> Products => _context.Products;

        public ProductReadRepository(ProductPriceStatisticsDbContext productPriceStatisticsDbContext)
        {
            if (productPriceStatisticsDbContext == null)
            {
                throw new ArgumentNullException($"Argument {nameof(productPriceStatisticsDbContext)} is null");
            }

            _context = productPriceStatisticsDbContext;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            foreach (var product in Products)
            {
                yield return new ProductDto(product.GlobalProductId, product.Name, null);
            }
        }
    }
}
