using System;

namespace ProductPriceStatistics.Domain.Entities
{
    public record Price
    {
        public decimal Value { get; init; }

        public DateTime DateTimeMeasure { get; init; }

        public Store Store { get; init; }

        public Price(decimal value, Store store, DateTime dateTimeMeasure) 
        {
            Value = value;
            Store = store;
            DateTimeMeasure = dateTimeMeasure;
        }
    }
}
