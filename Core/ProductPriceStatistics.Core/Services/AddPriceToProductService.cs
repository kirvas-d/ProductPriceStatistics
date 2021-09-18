using ProductPriceStatistics.Domain.Entities;
using ProductPriceStatistics.Domain.Repositories;
using System;

namespace ProductPriceStatistics.Domain.Services
{
    public class AddPriceToProductService : IAddPriceToProductService
    {
        private IProductStatisticsRepository _productStatisticsRepository;

        public AddPriceToProductService(IProductStatisticsRepository productStatisticsRepository)
        {
            if (productStatisticsRepository == null) 
            {
                throw new ArgumentNullException("productStatisticsRepository is null");
            }

            _productStatisticsRepository = productStatisticsRepository;
        }

        public void AddPriceToProduct(string productName, Price price) 
        {
            if (productName == null) 
            {
                throw new ArgumentNullException("productName is null");
            }

            if (price == null) 
            {
                throw new ArgumentNullException("price is null");
            }

            Guid? productId = _productStatisticsRepository.GetProductIdByName(productName);
            if (productId != null)
            {
                _productStatisticsRepository.AddPriceToProduct(productId.Value, price);
            }
            else 
            {
                Product newProduct = new Product(Guid.NewGuid(), productName);
                _productStatisticsRepository.AddProduct(newProduct);
                _productStatisticsRepository.AddPriceToProduct(newProduct.Id, price);
            }
        }
    }
}
