namespace ProductPriceStatistics.Core.Models
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
