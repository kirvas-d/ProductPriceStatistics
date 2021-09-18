namespace ProductPriceStatistics.Core.Models
{
    public record Tag
    {
        public string Name { get; init; }

        public Tag(string name) 
        {
            Name = name;
        }
    }
}
