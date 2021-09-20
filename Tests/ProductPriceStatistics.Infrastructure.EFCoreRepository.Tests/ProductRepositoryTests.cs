using Microsoft.EntityFrameworkCore;
using ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Repositories;
using ProductPriceStatistics.Infrastructure.EFCoreRepository;
using System;
using System.Linq;
using Xunit;

namespace ProductPriceStatistics.Infrastucture.EFCoreRepository.Tests
{

    public class ProductRepositoryTests: IDisposable
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductPriceStatisticsDbContext _context;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ProductPriceStatisticsDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;
            _context = new ProductPriceStatisticsDbContext(options);

            _productRepository = new ProductRepository(_context);
        }

        private (Guid productId, string productName) InitProductPriceStatisticsDbContext() 
        {
            Guid guid = Guid.NewGuid();
            string productName = $"Addproduct-{guid}";

            _context.Products.Add(new Infrastructure.EFCoreRepository.Models.Product()
            {
                GlobalProductId = guid,
                Name = productName
            });
            _context.SaveChanges();
            
            return (guid, productName);
        }

        [Fact]
        public void AddProductTest()
        {
            var product = new Product(Guid.NewGuid(), "Name1");

            _productRepository.AddProduct(product);

            var dbProduct = _context.Products.Where(p => p.GlobalProductId == product.ProductId && p.Name == product.Name).FirstOrDefault();
            Assert.NotNull(dbProduct);
        }

        [Fact]
        public void GetProductByIdTest() 
        {
            var initProduct = InitProductPriceStatisticsDbContext();

            var product = _productRepository.GetProductById(initProduct.productId);

            Assert.Equal(initProduct.productId, product.ProductId);
            Assert.Equal(initProduct.productName, product.Name);
        }

        [Fact]
        public void GetProductByNameTest()
        {
            var initProduct = InitProductPriceStatisticsDbContext();

            var product = _productRepository.GetProductByName(initProduct.productName);

            Assert.Equal(initProduct.productId, product.ProductId);
            Assert.Equal(initProduct.productName, product.Name);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
