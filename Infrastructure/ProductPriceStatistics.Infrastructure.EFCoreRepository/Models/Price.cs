using System;
using System.Collections.Generic;

#nullable disable

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository.Models
{
    class Price
    {
        public long PriceId { get; set; }
        public long ProductId { get; set; }
        public long StoreId { get; set; }
        public decimal Value { get; set; }
        public DateTime DateTimeStamp { get; set; }
        
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
