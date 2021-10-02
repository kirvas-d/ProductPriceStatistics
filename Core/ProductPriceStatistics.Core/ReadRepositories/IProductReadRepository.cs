using ProductPriceStatistics.Core.DTOs;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.ReadRepositories
{
    public interface IProductReadRepository
    {
        IEnumerable<ProductDto> GetAllProducts();
    }
}
