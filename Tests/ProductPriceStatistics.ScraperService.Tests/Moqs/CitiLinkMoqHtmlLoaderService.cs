using HtmlParser.HtmlLoaderService;
using System.Collections.Generic;
using System.IO;

namespace ProductPriceStatistics.ScraperService.Tests
{
    class CitiLinkMoqHtmlLoaderService : IHtmlLoaderService
    {
        private Dictionary<string, string> _uriHtmlBodyDictionary;

        public CitiLinkMoqHtmlLoaderService() 
        {
            _uriHtmlBodyDictionary = new Dictionary<string, string>();
            _uriHtmlBodyDictionary.Add("http://www.citilink.ru/0", File.ReadAllText(@"Pages\CitiLink.html"));
            _uriHtmlBodyDictionary.Add("http://www.citilink.ru/1", "<html><body><h1>Hello</h1></body></html>");
        }

        public string GetHtmlBody(string uri)
        {
            return _uriHtmlBodyDictionary[uri];
        }
    }
}
