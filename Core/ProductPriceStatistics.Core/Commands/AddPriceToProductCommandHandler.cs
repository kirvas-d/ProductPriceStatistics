using ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPriceStatistics.Core.Commands
{
    public class AddPriceToProductCommandHandler : ICommandHandler<AddPriceToProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IPriceRepository _priceRepository;

        public AddPriceToProductCommandHandler(IProductRepository productRepository, IPriceRepository priceRepository) 
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

        public void Handle(AddPriceToProductCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
