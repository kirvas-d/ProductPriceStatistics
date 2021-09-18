using System;

namespace ProductPriceStatistics.Core.Models
{
    public record Price
    {
        public Guid ProductId { get; init; }

        public decimal Value { get; init; }

        public DateTime DateTimeStamp { get; init; }

        public Store Store { get; init; }

        public Price(Guid productId, decimal value, Store store, DateTime dateTimeStamp) 
        {
            ProductId = productId;
            Value = value;
            Store = store;
            DateTimeStamp = dateTimeStamp;
        }
    }
}
