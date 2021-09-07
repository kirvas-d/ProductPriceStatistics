using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Domain.Entities
{
    public record Product
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        private List<Tag> _tags;

        public IReadOnlyCollection<Tag> Tags => _tags;

        public Product(Guid id, string name, IEnumerable<Tag> tags = null)
        {
            if (string.IsNullOrWhiteSpace(name)) 
            {
                throw new ArgumentNullException("Argument 'name' is null");
            }

            Id = id;
            Name = name;
            _tags = new List<Tag>(tags);
        }
    }
}
