using System;

namespace ProductPriceStatistics.ScraperService.Models
{
    public record ProductMeasure(string ProductName, decimal Price, string StoreName, DateTime DateTimeStamp);
}
