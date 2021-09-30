using ProductPriceStatistics.Core.DTOs;
using ProductPriceStatistics.Core.Queries;
using ProductPriceStatistics.Core.Repositories;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.QueryHandlers
{
    public class GetPricesOfProductQueryHandler : IQueryHandler<GetPricesOfProductQuery, IEnumerable<PriceDto>>
    {
        private readonly IPriceRepository _priceRepository;

        public GetPricesOfProductQueryHandler(IPriceRepository priceRepository) 
        {
            _priceRepository = priceRepository;
        }

        public IEnumerable<PriceDto> Ask(GetPricesOfProductQuery query)
        {
            if (query == null) throw new ArgumentNullException($"{nameof(query)} mustn't be null");

            return _priceRepository.GetPricesOfProduct(query.ProductId, query.StartDateTime, query.FinishDateTime);
        }
    }
}
