using ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Commands;
using ProductPriceStatistics.Core.Repositories;
using System;

namespace ProductPriceStatistics.Core.CommandHandlers
{
    public class AddPriceToProductCommandHandler : ICommandHandler<AddPriceToProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPriceRepository _priceRepository;

        public AddPriceToProductCommandHandler(IProductRepository productRepository, IPriceRepository priceRepository) 
        {
            if (productRepository == null)
            {
                throw new ArgumentNullException($"{nameof(productRepository)} mustn't be null");
            }

            if (priceRepository == null)
            {
                throw new ArgumentNullException($"{nameof(priceRepository)} mustn't be null");
            }

            _productRepository = productRepository;
            _priceRepository = priceRepository;
        }

        public void Handle(AddPriceToProductCommand command)
        {
            if (command == null) throw new ArgumentNullException($"{nameof(command)} mustn't be null");

            Product product = _productRepository.GetProductByName(command.ProductName);
            if (product == null)
            {
                product = new Product(Guid.NewGuid(), command.ProductName);
                _productRepository.AddProduct(product);
            }

            _priceRepository.AddPrice(new Price(product.ProductId, command.Price, new Store(command.StoreName), command.DateTimeStamp));
        }
    }
}
