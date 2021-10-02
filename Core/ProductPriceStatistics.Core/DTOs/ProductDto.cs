using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.DTOs
{
    record ProductDto(Guid ProductId, string Name, IEnumerable<string> Tags);
}
