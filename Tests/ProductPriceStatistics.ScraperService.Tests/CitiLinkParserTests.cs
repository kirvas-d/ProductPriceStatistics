using ProductPriceStatistics.ScraperService.Models;
using ProductPriceStatistics.ScraperService.ParserSiteServices;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProductPriceStatistics.ScraperService.Tests
{
    public class CitiLinkParserTests
    {
        [Fact]
        public void TestGetEntity()
        {
            var productMeassure = new ProductMeasure
                (
                    ProductName: "Видеокарта PALIT NVIDIA  GeForce RTX 3060,  PA-RTX3060 DUAL 12G",
                    Price: 71590,
                    StoreName: "CitiLink",
                    DateTimeStamp: DateTime.Now
                );

            CitiLinkParser citiLinkParser = new CitiLinkParser(new CitiLinkMoqHtmlLoaderService(), new ParserConfiguration.CitiLinkPageHtmlParserConfiguration() { Uri = "http://www.citilink.ru/{page}" });

            List<ProductMeasure> parserProductMeassure = new List<ProductMeasure>();
            foreach (var entity in citiLinkParser.GetEntitys())
            {
                parserProductMeassure.Add(entity);
            }

            Assert.True(parserProductMeassure.Count == 1);
            Assert.Equal(productMeassure.ProductName, parserProductMeassure.First().ProductName);
            Assert.Equal(productMeassure.Price, parserProductMeassure.First().Price);
            Assert.Equal(productMeassure.StoreName, parserProductMeassure.First().StoreName);
        } 
    }
}
