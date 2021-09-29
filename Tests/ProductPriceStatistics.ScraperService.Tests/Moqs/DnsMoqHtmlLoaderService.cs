using HtmlParser.HtmlLoaderService;
using System.Collections.Generic;
using System.IO;

namespace ProductPriceStatistics.ScraperService.Tests.Moqs
{
    class DnsMoqHtmlLoaderService : IHtmlLoaderService
    {
        private Dictionary<string, string> _uriHtmlBodyDictionary;

        public DnsMoqHtmlLoaderService()
        {
            _uriHtmlBodyDictionary = new Dictionary<string, string>();
            _uriHtmlBodyDictionary.Add("http://www.dns.ru/0", File.ReadAllText(@"Pages\Dns.html"));
            _uriHtmlBodyDictionary.Add("http://www.dns.ru/1", "<html><body><h1>Hello</h1></body></html>");
        }

        public string GetHtmlBody(string uri)
        {
            return _uriHtmlBodyDictionary[uri];
        }
    }
}
