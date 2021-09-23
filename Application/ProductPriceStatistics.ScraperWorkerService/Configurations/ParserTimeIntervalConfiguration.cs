using System;

namespace ProductPriceStatistics.ScraperWorkerService.Configurations
{
    public class ParserTimeIntervalConfiguration
    {
        public const string ConfigurationKey = "ParserTimeIntervalConfiguration";

        public TimeSpan IntervalTimeSpan { get; set; }
    }
}
