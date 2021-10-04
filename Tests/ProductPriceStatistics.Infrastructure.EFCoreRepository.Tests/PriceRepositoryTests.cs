using Microsoft.EntityFrameworkCore;
using ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository.Tests
{

    public class PriceRepositoryTests: IDisposable
    {
        private readonly IPriceRepository _priceRepository;
        private readonly ProductPriceStatisticsDbContext _context;

        public PriceRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ProductPriceStatisticsDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;
            _context = new ProductPriceStatisticsDbContext(options);

            _priceRepository = new PriceRepository(_context);
        }

        public Product InitProductInContext() 
        {
            Guid guid = Guid.NewGuid();
            Product product = new Product(guid, $"Name-{guid}");
            _context.Products.Add(new Models.Product()
            {
                GlobalProductId = product.ProductId,
                Name = product.Name
            });
            _context.SaveChanges();

            return product;
        }

        private IEnumerable<Price> InitPricesInContext() 
        {
            Product product = InitProductInContext();
            Store store = new Store("Store");

            List<Price> prices = new List<Price>()
            {
                new Price(product.ProductId, 100, store, DateTime.Now),
                new Price(product.ProductId, 200, store, DateTime.Now),
                new Price(product.ProductId, 300, store, DateTime.Now),
                new Price(product.ProductId, 400, store, DateTime.Now),
                new Price(product.ProductId, 500, store, DateTime.Now),
            };

            foreach (var price in prices)
            {
                _priceRepository.AddPrice(price);
            }

            return prices;
        }

        [Fact]
        public void AddPriceErrorTest()
        {
            Price price = new Price(Guid.NewGuid(), 100, new Store("1"), DateTime.Now);

            Assert.Throws<Exception>(() =>
            {
                _priceRepository.AddPrice(price);
            });
        }

        [Fact]
        public void AddPriceTest() 
        {
            Product product = InitProductInContext();
            Price price = new Price(product.ProductId, 100, new Store("store"), DateTime.Now);

            _priceRepository.AddPrice(price);

            var dbPrice = _context.Prices.Where(p => p.Product.GlobalProductId == product.ProductId).FirstOrDefault();
            Assert.NotNull(dbPrice);
        }

        [Fact]
        public void GetAllPricesTest() 
        {
            IEnumerable<Price> prices = InitPricesInContext();
            IEnumerable<Price> pricesFromDb = _priceRepository.GetAllPrices();

            foreach (var price in prices) 
            {
                var priceFromDb = pricesFromDb.Where(p => p.ProductId == price.ProductId &&
                                                     p.Store == price.Store &&
                                                     p.Value == price.Value &&
                                                     p.DateTimeStamp == p.DateTimeStamp).FirstOrDefault();

                Assert.NotNull(priceFromDb);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
