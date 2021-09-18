using Microsoft.EntityFrameworkCore;
using ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Repositories;
using ProductPriceStatistics.Infrastructure.EFCoreRepository;
using System;
using Xunit;

namespace ProductPriceStatistics.Infrastucture.EFCoreRepository.Tests
{

    public class ProductRepositoryTests
    {
        private readonly IProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ProductPriceStatisticsDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            _productRepository = new ProductRepository(options);
        }

        [Fact]
        public void AddProductTest() 
        {
            var guid = Guid.NewGuid();
            var product = new Product(guid, "Name1");

            _productRepository.AddProduct(product);

            var productFromDb = _productRepository.GetProductById(guid);

            Assert.Equal(productFromDb.Name, product.Name);
        }
    }
}
