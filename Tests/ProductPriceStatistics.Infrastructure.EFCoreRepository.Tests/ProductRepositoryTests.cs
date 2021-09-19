using Microsoft.EntityFrameworkCore;
using ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Repositories;
using ProductPriceStatistics.Infrastructure.EFCoreRepository;
using System;
using System.Linq;
using Xunit;

namespace ProductPriceStatistics.Infrastucture.EFCoreRepository.Tests
{

    public class ProductRepositoryTests
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductPriceStatisticsDbContext _productPriceStatisticsDbContext;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ProductPriceStatisticsDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;
            _productPriceStatisticsDbContext = new ProductPriceStatisticsDbContext(options);

            _productRepository = new ProductRepository(_productPriceStatisticsDbContext);
        }

        private (Guid productId, string productName) InitProductPriceStatisticsDbContest() 
        {
            Guid guid = Guid.NewGuid();
            string productName = $"Addproduct-{guid}";

            _productPriceStatisticsDbContext.Products.Add(new Infrastructure.EFCoreRepository.Models.Product()
            {
                GlobalProductId = guid,
                Name = productName
            });
            _productPriceStatisticsDbContext.SaveChanges();
            
            return (guid, productName);
        }

        [Fact]
        public void AddProductTest()
        {
            var product = new Product(Guid.NewGuid(), "Name1");

            _productRepository.AddProduct(product);

            var dbProduct = _productPriceStatisticsDbContext.Products.Where(p => p.GlobalProductId == product.ProductId && p.Name == product.Name).FirstOrDefault();
            Assert.NotNull(dbProduct);
        }

        [Fact]
        public void GetProductByIdTest() 
        {
            var initProduct = InitProductPriceStatisticsDbContest();

            var product = _productRepository.GetProductById(initProduct.productId);

            Assert.Equal(initProduct.productId, product.ProductId);
            Assert.Equal(initProduct.productName, product.Name);
        }

        [Fact]
        public void GetProductByNameTest()
        {
            var initProduct = InitProductPriceStatisticsDbContest();

            var product = _productRepository.GetProductByName(initProduct.productName);

            Assert.Equal(initProduct.productId, product.ProductId);
            Assert.Equal(initProduct.productName, product.Name);
        }
    }
}
