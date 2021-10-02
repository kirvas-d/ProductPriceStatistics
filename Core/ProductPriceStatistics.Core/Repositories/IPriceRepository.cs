using ProductPriceStatistics.Core.DTOs;
using ProductPriceStatistics.Core.Models;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.Repositories
{
    public interface IPriceRepository
    {
        void AddPrice(Price price);

        IEnumerable<Price> GetAllPrices();
    }
}
