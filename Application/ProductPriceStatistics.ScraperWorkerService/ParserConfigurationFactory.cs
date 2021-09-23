using HtmlParser;
using ProductPriceStatistics.ScraperService.ParserConfiguration;
using ProductPriceStatistics.ScraperWorkerService.Configurations;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.ScraperWorkerService
{
    class ParserConfigurationFactory
    {
        public static IEnumerable<PageHtmlParserConfiguration> CreteParserConfiguration(IEnumerable<ParserConfiguration> parserConfigurations) 
        {
            foreach (var parserConfigurationItem in parserConfigurations) 
            {
                PageHtmlParserConfiguration pageHtmlParserConfiguration = null;
                switch (parserConfigurationItem.TypeParser) 
                {
                    case TypeParser.CitiLink:
                        pageHtmlParserConfiguration = new CitiLinkPageHtmlParserConfiguration()
                        {
                            Uri = parserConfigurationItem.Uri
                        };
                        break;
                    case TypeParser.DNS:
                        pageHtmlParserConfiguration = new DnsPageHtmlParserConfiguration()
                        {
                            Uri = parserConfigurationItem.Uri
                        };
                        break;
                    case TypeParser.Ozon:
                        pageHtmlParserConfiguration = new OzonPageHtmlParserConfiguration()
                        {
                            Uri = parserConfigurationItem.Uri
                        };
                        break;
                    case TypeParser.Pleer:
                        pageHtmlParserConfiguration = new PleerPageHtmlParserConfiguration()
                        {
                            Uri = parserConfigurationItem.Uri
                        };
                        break;
                    default:
                        throw new Exception($"{parserConfigurationItem.TypeParser} is undifuned value for ParserConfigurationFactory");                     
                }

                yield return pageHtmlParserConfiguration;
            }
        }
    }
}
