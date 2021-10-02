using ProductPriceStatistics.Core.DTOs;
using ProductPriceStatistics.Core.Queries;
using ProductPriceStatistics.Core.ReadRepositories;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Core.QueryHandlers
{
    public class GetPricesOfProductQueryHandler : IQueryHandler<GetPricesOfProductQuery, IEnumerable<PriceDto>>
    {
        private readonly IPriceReadRepository _priceReadRepository;

        public GetPricesOfProductQueryHandler(IPriceReadRepository priceReadRepository) 
        {
            _priceReadRepository = priceReadRepository;
        }

        public IEnumerable<PriceDto> Ask(GetPricesOfProductQuery query)
        {
            if (query == null) throw new ArgumentNullException($"{nameof(query)} mustn't be null");

            return _priceReadRepository.GetPricesOfProduct(query.ProductId, query.StartDateTime, query.FinishDateTime);
        }
    }
}
