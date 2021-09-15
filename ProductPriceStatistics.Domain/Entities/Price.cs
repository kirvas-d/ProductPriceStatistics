using System;

namespace ProductPriceStatistics.Domain.Entities
{
    public record Price
    {
        public Guid ProductId { get; init; }

        public decimal Value { get; init; }

        public DateTime DateTimeMeasure { get; init; }

        public Store Store { get; init; }

        public Price(Guid productId, decimal value, Store store, DateTime dateTimeMeasure) 
        {
            ProductId = productId;
            Value = value;
            Store = store;
            DateTimeMeasure = dateTimeMeasure;
        }
    }
}
