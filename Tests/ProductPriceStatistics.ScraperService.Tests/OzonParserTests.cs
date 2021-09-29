using ProductPriceStatistics.ScraperService.Models;
using ProductPriceStatistics.ScraperService.ParserSiteServices;
using ProductPriceStatistics.ScraperService.Tests.Moqs;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProductPriceStatistics.ScraperService.Tests
{
    public class OzonParserTests
    {
        [Fact]
        public void TestGetEntity()
        {
            var productMeassure = new ProductMeasure
                (
                    ProductName: "Видеокарта ASUS GeForce RTX 3060 Ti 8 ГБ (DUAL-RTX3060TI-8G-MINI-V2), rev. 2.0 (LHR)",
                    Price: 78279,
                    StoreName: "Ozon",
                    DateTimeStamp: DateTime.Now
                );

            var ozonParser = new OzonParser(new OzonMoqHtmlLoaderService(), new ParserConfiguration.OzonPageHtmlParserConfiguration() { Uri = "http://www.ozon.ru/{page}" });

            List<ProductMeasure> parserProductMeassure = new List<ProductMeasure>();
            foreach (var entity in ozonParser.GetEntitys())
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
