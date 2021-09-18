using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using HtmlParser;
using HtmlParser.HtmlLoaderService;
using ProductPriceStatistics.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProductPriceStatistics.ScraperService.ParserSiteServices
{
    class DnsParser : AbstractSequentialProcessPageHtmlParser<ProductMeasure>
    {
        private readonly IBrowsingContext _browsingContext;
        private readonly Regex regPrice = new Regex(@"[\d-., ]*", RegexOptions.Compiled);

        public DnsParser(IHtmlLoaderService htmlLoaderService, PageHtmlParserConfiguration configure) : base(htmlLoaderService, configure)
        {
            _browsingContext = BrowsingContext.New();
        }

        protected override IEnumerable<ProductMeasure> GetEntityFromPage(string htmlBody)
        {
            var openPageTask = _browsingContext.OpenAsync(r => r.Content(htmlBody));
            openPageTask.Wait();
            IDocument htmlDocument = openPageTask.Result;
            var products = htmlDocument.QuerySelectorAll("div.catalog-product");
            foreach (HtmlElement product in products)
            {
                string name = product.QuerySelector("a.ui-link").InnerHtml;
                string stringprice = product.QuerySelector("div.product-min-price__current")?.TextContent;
                decimal price = Convert.ToDecimal(regPrice.Match(stringprice).Value);
                
                yield return new ProductMeasure(name, new Price(price, new Store("DNS"), DateTime.Now));
            }
        }
    }
}
