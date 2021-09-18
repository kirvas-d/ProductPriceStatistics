using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using HtmlParser;
using HtmlParser.HtmlLoaderService;
using Newtonsoft.Json;
using ProductPriceStatistics.Core.Models;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.ScraperService.ParserSiteServices
{
    public class CitiLinkParser : AbstractSequentialProcessPageHtmlParser<ProductMeasure>
    {
        private readonly IBrowsingContext _browsingContext;

        public CitiLinkParser(IHtmlLoaderService htmlLoaderService, PageHtmlParserConfiguration configure) : base(htmlLoaderService, configure)
        {
            _browsingContext = BrowsingContext.New();
        }

        protected override IEnumerable<ProductMeasure> GetEntityFromPage(string htmlBody)
        {
            var openPageTask = _browsingContext.OpenAsync(r => r.Content(htmlBody));
            openPageTask.Wait();
            IDocument htmlDocument = openPageTask.Result;
            var products = htmlDocument.QuerySelectorAll("div.ProductCardHorizontal");
            foreach (IElement product in products)
            {
                HtmlElement htmlElement = product as HtmlElement;
                if (htmlElement != null)
                {
                    string json = htmlElement.Dataset["params"];
                    dynamic param = JsonConvert.DeserializeObject(json);
                    string name = param.shortName;
                    decimal price = (decimal)param.price;

                    yield return new ProductMeasure(name, new Price(price, new Store("CitiLink"), DateTime.Now));
                }
            }
        }
    }
}
