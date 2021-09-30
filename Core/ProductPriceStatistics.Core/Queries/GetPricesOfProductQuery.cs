using System;

namespace ProductPriceStatistics.Core.Queries
{
    public class GetPricesOfProductQuery
    {
        public Guid ProductId { get; init; }
        public DateTime? StartDateTime { get; init; }
        public DateTime? FinishDateTime { get; init; }

        public GetPricesOfProductQuery(Guid productId, DateTime? startDateTime, DateTime? finishDateTime) 
        {
            ProductId = productId;
            StartDateTime = StartDateTime;
            FinishDateTime = FinishDateTime;
        }
    }
}
