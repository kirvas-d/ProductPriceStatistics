using ProductPriceStatistics.ScraperService.Models;
using ProductPriceStatistics.ScraperService.ParserSiteServices;
using ProductPriceStatistics.ScraperService.Tests.Moqs;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProductPriceStatistics.ScraperService.Tests
{
    public class PleerParserTests
    {
        [Fact]
        public void TestGetEntity()
        {
            var productMeassure = new ProductMeasure
                (
                    ProductName: "Intel Core I3-10100F (3600MHz/LGA1200/L3 6144Kb) OEM",
                    Price: 6573,
                    StoreName: "Pleer",
                    DateTimeStamp: DateTime.Now
                );

            var pleerParser = new PleerParser(new PleerMoqHtmlLoaderService(), new ParserConfiguration.PleerPageHtmlParserConfiguration() { Uri = "http://www.pleer.ru/{page}" });

            List<ProductMeasure> parserProductMeassure = new List<ProductMeasure>();
            foreach (var entity in pleerParser.GetEntitys())
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
