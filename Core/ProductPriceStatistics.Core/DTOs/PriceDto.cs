using System;

namespace ProductPriceStatistics.Core.DTOs
{
    public record PriceDto(
        decimal Value, 
        string StoreName, 
        DateTime DateTimeStamp);
}
