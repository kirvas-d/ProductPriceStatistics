using AngleSharp;
using AngleSharp.Dom;
using HtmlParser;
using HtmlParser.HtmlLoaderService;
using ProductPriceStatistics.ScraperService.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProductPriceStatistics.ScraperService.ParserSiteServices
{
    class OzonParser : AbstractSequentialProcessPageHtmlParser<ProductMeasure>
    {
        private readonly IBrowsingContext _browsingContext;
        private readonly Regex regPrice = new Regex(@"[\d-.,\s]*", RegexOptions.Compiled);
        private readonly Regex replace = new Regex(@"[\s]*", RegexOptions.Compiled);

        public OzonParser(IHtmlLoaderService htmlLoaderService, PageHtmlParserConfiguration configure) : base(htmlLoaderService, configure)
        {
            _browsingContext = BrowsingContext.New();
        }
        protected override IEnumerable<ProductMeasure> GetEntityFromPage(string htmlBody)
        {
            var openPageTask = _browsingContext.OpenAsync(r => r.Content(htmlBody));
            openPageTask.Wait();
            IDocument htmlDocument = openPageTask.Result;
            var products = htmlDocument.QuerySelectorAll("div.a0c4");

            foreach (var product in products)
            {
                string name = null;
                decimal? price = null;
                try
                {
                    name = product.QuerySelector("a.a2g0").TextContent;
                    string stringPrice = product.QuerySelector("span.b5v6").TextContent;
                    var val = replace.Replace(regPrice.Match(stringPrice).Value, string.Empty);
                    price = Convert.ToDecimal(val);
                }
                catch 
                {

                }

                if (name != null && price != null)
                {
                    yield return new ProductMeasure(name, price.Value, "OZON", DateTime.Now);
                }
            }
        }
    }
}
