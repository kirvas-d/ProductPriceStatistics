using HtmlParser;
using ProductPriceStatistics.ScraperService.ParserConfiguration;
using System.Collections.Generic;

namespace ProductPriceStatistics.ScraperWorkerService.Configurations
{
    class ParserConfiguration
    {
        public IEnumerable<PageHtmlParserConfiguration> Configuration => new List<PageHtmlParserConfiguration>()
        {
            
        };
    }
}
