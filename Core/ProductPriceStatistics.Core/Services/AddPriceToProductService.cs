using ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Repositories;
using System;

namespace ProductPriceStatistics.Domain.Services
{
    public class AddPriceToProductService : IAddPriceToProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPriceRepository _priceRepository;

        public AddPriceToProductService(IProductRepository productRepository, IPriceRepository priceRepository)
        {
            if (productRepository == null)
            {
                throw new ArgumentNullException($"{nameof(productRepository)} is null");
            }

            if (priceRepository == null)
            {
                throw new ArgumentNullException($"{nameof(priceRepository)} is null");
            }

            _productRepository = productRepository;
            _priceRepository = priceRepository;
        }

        public void AddPriceToProduct(string productName, decimal price, string storeName, DateTime dateTimeStamp) 
        {
            if (productName == null) 
            {
                throw new ArgumentNullException("productName is null");
            }

            if (price == null) 
            {
                throw new ArgumentNullException("price is null");
            }

            Product product = _productRepository.GetProductByName(productName);
            if (product == null)
            {
                product = new Product(Guid.NewGuid(), productName);
                _productRepository.AddProduct(product);
            }        

            _priceRepository.AddPrice(new Price(product.ProductId, price, new Store(storeName), dateTimeStamp));
        }
    }
}
