using HtmlParser;
using ProductPriceStatistics.ScraperService.ParserConfiguration;
using System.Collections.Generic;

namespace ProductPriceStatistics.ScraperWorkerService
{
    class ParserCongiguration
    {
        public IEnumerable<PageHtmlParserConfiguration> Configuration => new List<PageHtmlParserConfiguration>()
        {
            new CitiLinkPageHtmlParserConfiguration()
            {
                Uri = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/cpu/?p={page}"
            },
            new CitiLinkPageHtmlParserConfiguration()
            {
                Uri = "https://www.citilink.ru/catalog/computers_and_notebooks/parts/videocards/?p={page}"
            },
            new DnsPageHtmlParserConfiguration()
            {
                Uri = "https://www.dns-shop.ru/catalog/17a899cd16404e77/processory/?p={page}"
            },
            new DnsPageHtmlParserConfiguration()
            {
                Uri = "https://www.dns-shop.ru/catalog/17a899cd16404e77/videokarty/?p={page}"
            },
            new PleerPageHtmlParserConfiguration()
            {
                Uri = "https://www.pleer.ru/list_videokarty_page{page}.html"
            },
            new PleerPageHtmlParserConfiguration()
            {
                Uri = "https://www.pleer.ru/list_socketlga1150_page{page}.html"
            }
        };
    }
}
