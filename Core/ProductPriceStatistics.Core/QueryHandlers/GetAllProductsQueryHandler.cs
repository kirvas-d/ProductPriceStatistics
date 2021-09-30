using ProductPriceStatistics.Core.Models;
using ProductPriceStatistics.Core.Repositories;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.QueryHandlers
{
    class GetAllProductsQueryHandler : IQueryHandler<IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> Ask()
        {
            return _productRepository.GetAllProducts();
        }
    }
}
