using ProductPriceStatistics.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Domain.Repositories
{
    public interface IPriceRepository
    {
        void AddPrice(Price price);

        IEnumerable<Price> GetAllPrices();

        IEnumerable<Price> GetPricesOfProduct(Guid productId, DateTime? startDateTimeStamp, DateTime? finishDateTimeStamp);
    }
}
