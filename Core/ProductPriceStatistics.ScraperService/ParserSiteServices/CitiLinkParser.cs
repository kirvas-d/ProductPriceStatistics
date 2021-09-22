using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using HtmlParser;
using HtmlParser.HtmlLoaderService;
using Newtonsoft.Json;
using ProductPriceStatistics.ScraperService.Models;
using ProductPriceStatistics.ScraperService.ParserConfiguration;
using System;
using System.Collections.Generic;

namespace ProductPriceStatistics.ScraperService.ParserSiteServices
{
    public class CitiLinkParser : AbstractSequentialProcessPageHtmlParser<ProductMeasure>
    {
        private readonly IBrowsingContext _browsingContext;

        public CitiLinkParser(IHtmlLoaderService htmlLoaderService, CitiLinkPageHtmlParserConfiguration configure) : base(htmlLoaderService, configure)
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
                    string name = null;
                    decimal? price = null;
                    try
                    {
                        string json = htmlElement.Dataset["params"];
                        dynamic param = JsonConvert.DeserializeObject(json);
                        name = param.shortName;
                        price = (decimal)param.price;
                    }
                    catch(Exception e)
                    {

                    }

                    if (name != null && price != null)
                    {
                        yield return new ProductMeasure(name, price.Value, "CitiLink", DateTime.Now);
                    }
                }
            }
        }
    }
}
