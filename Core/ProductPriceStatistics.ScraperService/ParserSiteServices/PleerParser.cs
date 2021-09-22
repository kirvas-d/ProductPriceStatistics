using AngleSharp;
using AngleSharp.Dom;
using HtmlParser;
using HtmlParser.HtmlLoaderService;
using ProductPriceStatistics.ScraperService.Models;
using ProductPriceStatistics.ScraperService.ParserConfiguration;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProductPriceStatistics.ScraperService.ParserSiteServices
{
    class PleerParser : AbstractSequentialProcessPageHtmlParser<ProductMeasure>
    {
        private readonly IBrowsingContext _browsingContext;
        private readonly Regex regPrice = new Regex(@"[\d-., ]*", RegexOptions.Compiled);

        public PleerParser(IHtmlLoaderService htmlLoaderService, PleerPageHtmlParserConfiguration configure) : base(htmlLoaderService, configure)
        {
            _browsingContext = BrowsingContext.New();
        }

        protected override IEnumerable<ProductMeasure> GetEntityFromPage(string htmlBody)
        {
            var openPageTask = _browsingContext.OpenAsync(r => r.Content(htmlBody));
            openPageTask.Wait();
            IDocument htmlDocument = openPageTask.Result;
            var products = htmlDocument.QuerySelectorAll("div.section_item");
            foreach (var product in products)
            {
                string name = null;
                decimal? price = null;

                try
                {
                    name = product.QuerySelector("span.item_name").InnerHtml;
                    string textPrice = product.QuerySelector("div.price")?.TextContent;
                    price = Convert.ToDecimal(regPrice.Match(textPrice).Value);
                }
                catch 
                {

                }

                if (name != null && price != null)
                {
                    yield return new ProductMeasure(name, price.Value, "Pleer", DateTime.Now);
                }
            }
        }
    }
}
