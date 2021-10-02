using ProductPriceStatistics.Core.DTOs;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.ReadRepositories
{
    public interface IPriceReadRepository
    {
        IEnumerable<PriceDto> GetPricesOfProduct(Guid productId, DateTime? startDateTimeStamp, DateTime? finishDateTimeStamp);
    }
}
