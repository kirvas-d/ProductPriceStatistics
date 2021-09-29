using HtmlParser.HtmlLoaderService;
using System.Collections.Generic;
using System.IO;

namespace ProductPriceStatistics.ScraperService.Tests.Moqs
{
    class OzonMoqHtmlLoaderService : IHtmlLoaderService
    {
        private Dictionary<string, string> _uriHtmlBodyDictionary;

        public OzonMoqHtmlLoaderService()
        {
            _uriHtmlBodyDictionary = new Dictionary<string, string>();
            _uriHtmlBodyDictionary.Add("http://www.ozon.ru/0", File.ReadAllText(@"Pages\Ozon.html"));
            _uriHtmlBodyDictionary.Add("http://www.ozon.ru/1", "<html><body><h1>Hello</h1></body></html>");
        }

        public string GetHtmlBody(string uri)
        {
            return _uriHtmlBodyDictionary[uri];
        }
    }
}
