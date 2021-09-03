namespace ProductPriceStatistics.Domain.Entities
{
    public record Store
    {
        public string Name { get; init; }

        public Store(string name)
        {
            Name = name;
        }
    }
}
