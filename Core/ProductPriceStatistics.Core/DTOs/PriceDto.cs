using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductPriceStatistics.Core.DTOs
{
    public record PriceDto(decimal Value, string StoreName, DateTime DateTimeStamp);
}
