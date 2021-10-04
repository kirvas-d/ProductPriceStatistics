using ProductPriceStatistics.Core.DTOs;
using ProductPriceStatistics.Core.ReadRepositories;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.QueryHandlers
{
    public class GetAllProductsQueryHandler : IQueryHandler<IEnumerable<ProductDto>>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository) 
        {
            _productReadRepository = productReadRepository;
        }

        public IEnumerable<ProductDto> Ask()
        {
            return _productReadRepository.GetAllProducts();
        }
    }
}
