using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.Domain.Entities
{
    public record Product
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        private List<Tag> _tags;

        public IReadOnlyCollection<Tag> Tags 
        {
            get 
            {
                return _tags;
            }
        }

        public Product(string name, Guid id, IEnumerable<Tag> tags)
        {
            Name = name;
            Id = id;
            _tags = new List<Tag>(tags);
        }
    }
}
