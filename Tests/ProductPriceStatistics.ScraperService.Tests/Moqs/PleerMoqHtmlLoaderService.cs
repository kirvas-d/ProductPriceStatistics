using HtmlParser.HtmlLoaderService;
using System.Collections.Generic;
using System.IO;

namespace ProductPriceStatistics.ScraperService.Tests.Moqs
{
    class PleerMoqHtmlLoaderService : IHtmlLoaderService
    {
        private Dictionary<string, string> _uriHtmlBodyDictionary;

        public PleerMoqHtmlLoaderService()
        {
            _uriHtmlBodyDictionary = new Dictionary<string, string>();
            _uriHtmlBodyDictionary.Add("http://www.pleer.ru/0", File.ReadAllText(@"Pages\Pleer.html"));
            _uriHtmlBodyDictionary.Add("http://www.pleer.ru/1", "<html><body><h1>Hello</h1></body></html>");
        }

        public string GetHtmlBody(string uri)
        {
            return _uriHtmlBodyDictionary[uri];
        }
    }
}
