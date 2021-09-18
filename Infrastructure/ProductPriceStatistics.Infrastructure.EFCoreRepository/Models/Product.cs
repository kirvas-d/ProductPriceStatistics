using System;
using System.Collections.Generic;

#nullable disable

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository.Models
{ 
    public class Product
    {
        public long Id { get; set; }
        public Guid GlobalProductId { get; set; }
        public string Name { get; set; }
        public ICollection<Price> Prices { get; set; }
        //public ICollection<ProductTag> ProductTags { get; set;}
        public ICollection<Tag> Tags { get; set; }

        public Product()
        {
            Prices = new HashSet<Price>();          
        }
    }
}
