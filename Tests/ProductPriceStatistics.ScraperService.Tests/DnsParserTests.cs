using ProductPriceStatistics.ScraperService.Models;
using ProductPriceStatistics.ScraperService.ParserSiteServices;
using ProductPriceStatistics.ScraperService.Tests.Moqs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductPriceStatistics.ScraperService.Tests
{
    public class DnsParserTests
    {
        [Fact]
        public void TestGetEntity()
        {
            var productMeassure = new ProductMeasure
                (
                    ProductName: "Видеокарта MSI GeForce 210 [N210-1GD3/LP] [PCI-E 2.0, 1 ГБ GDDR3, 64 бит, 460 МГц, DVI-D, HDMI, VGA (D-Sub)]",
                    Price: 3599,
                    StoreName: "DNS",
                    DateTimeStamp: DateTime.Now
                );

            var dnsParser = new DnsParser(new DnsMoqHtmlLoaderService(), new ParserConfiguration.DnsPageHtmlParserConfiguration() { Uri = "http://www.dns.ru/{page}" });

            List<ProductMeasure> parserProductMeassure = new List<ProductMeasure>();
            foreach (var entity in dnsParser.GetEntitys())
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
