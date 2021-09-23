using HtmlParser;
using ProductPriceStatistics.ScraperService.ParserConfiguration;
using System.Collections.Generic;

namespace ProductPriceStatistics.ScraperWorkerService.Configurations
{
    enum TypeParser {CitiLink, Pleer, DNS, Ozon }

    class ParserConfiguration
    {
        public const string ConfigurationKey = "ParserConfiguration";

        public TypeParser TypeParser { get; set; }

        public string Uri { get; set; }
    }
}
