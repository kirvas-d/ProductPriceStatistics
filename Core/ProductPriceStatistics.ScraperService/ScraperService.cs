using HtmlParser;
using HtmlParser.HtmlLoaderService;
using ProductPriceStatistics.ScraperService.Models;
using ProductPriceStatistics.ScraperService.ParserConfiguration;
using ProductPriceStatistics.ScraperService.ParserSiteServices;
using System.Collections.Generic;

namespace ProductPriceStatistics.ScraperService
{
    public class ScraperService
    {
        private readonly List<PageHtmlParserConfiguration> _parserConfigurations;
        private readonly List<AbstractSequentialProcessPageHtmlParser<ProductMeasure>> _parsers;

        public ScraperService(IHtmlLoaderService htmlLoaderService, IEnumerable<PageHtmlParserConfiguration> parserConfigurations) 
        {
            _parserConfigurations = new List<PageHtmlParserConfiguration>(parserConfigurations);
            _parsers = new List<AbstractSequentialProcessPageHtmlParser<ProductMeasure>>();

            foreach (PageHtmlParserConfiguration configuration in _parserConfigurations) 
            {
                switch (configuration) 
                {
                    case CitiLinkPageHtmlParserConfiguration:
                        _parsers.Add(new CitiLinkParser(htmlLoaderService, configuration as CitiLinkPageHtmlParserConfiguration));
                        break;
                    case DnsPageHtmlParserConfiguration:
                        _parsers.Add(new DnsParser(htmlLoaderService, configuration as DnsPageHtmlParserConfiguration));
                        break;
                    case OzonPageHtmlParserConfiguration:
                        _parsers.Add(new OzonParser(htmlLoaderService, configuration as OzonPageHtmlParserConfiguration));
                        break;
                    case PleerPageHtmlParserConfiguration:
                        _parsers.Add(new PleerParser(htmlLoaderService, configuration as PleerPageHtmlParserConfiguration));
                        break;
                }
            }
        }

        public IEnumerable<ProductMeasure> ScrapeProducts() 
        {
            foreach (var parser in _parsers)
            {
                foreach (var product in parser.GetEntitys())
                {
                    yield return product;
                }
            }
        }
    }
}
