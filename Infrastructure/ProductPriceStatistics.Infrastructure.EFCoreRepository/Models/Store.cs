using System;
using System.Collections.Generic;

#nullable disable

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository.Models
{
    class Store
    {
        public long StoreId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Price> Prices { get; set; }

        public Store()
        {
            Prices = new HashSet<Price>();
        }
    }
}
