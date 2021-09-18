using System.Collections.Generic;

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository.Models
{
    class Tag
    {
        public long TagId { get; set; }
        public long Name { get; set; }
        //public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<Product> Products { get; set; }

        public Tag()
        {
            //Products = new HashSet<Product>();
        }
    }
}
