using Microsoft.EntityFrameworkCore;
using ProductPriceStatistics.Core.Repositories;
using System;
using System.Collections.Generic;
using DbModels = ProductPriceStatistics.Infrastructure.EFCoreRepository.Models;
using CoreModels = ProductPriceStatistics.Core.Models;
using System.Linq;

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly ProductPriceStatisticsDbContext _context;

        private DbSet<DbModels.Product> Products => _context.Products;

        public ProductRepository(ProductPriceStatisticsDbContext productPriceStatisticsDbContext)
        {
            if (productPriceStatisticsDbContext == null) 
            {
                throw new ArgumentNullException($"Argument {nameof(productPriceStatisticsDbContext)} is null");
            }

            _context = productPriceStatisticsDbContext;
        }

        public void AddProduct(CoreModels.Product product)
        {
            Products.Add(new DbModels.Product()
            {
                GlobalProductId = product.ProductId,
                Name = product.Name
            });
            _context.SaveChanges();
        }

        public IEnumerable<CoreModels.Product> GetAllProducts()
        {
            foreach (var product in Products)
            {
                yield return new CoreModels.Product(product.GlobalProductId, product.Name);
            }
        }

        public CoreModels.Product GetProductById(Guid productId)
        {
            var dbProduct = Products.Where(p => p.GlobalProductId == productId).FirstOrDefault();

            CoreModels.Product coreProduct = null;
            if (dbProduct != null)
            {
                coreProduct = new CoreModels.Product(dbProduct.GlobalProductId, dbProduct.Name);
            }

            return coreProduct;
        }

        public CoreModels.Product GetProductByName(string productName)
        {
            var dbProduct = Products.Where(p => p.Name == productName).FirstOrDefault();

            CoreModels.Product coreProduct = null;
            if (dbProduct != null)
            {
                coreProduct = new CoreModels.Product(dbProduct.GlobalProductId, dbProduct.Name);
            }

            return coreProduct;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
