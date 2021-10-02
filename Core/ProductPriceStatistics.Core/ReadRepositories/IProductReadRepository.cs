using ProductPriceStatistics.Core.DTOs;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.ReadRepositories
{
    interface IProductReadRepository
    {
        IEnumerable<ProductDto> GetAllProducts();
    }
}
