namespace ProductPriceStatistics.Domain.Entities
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
