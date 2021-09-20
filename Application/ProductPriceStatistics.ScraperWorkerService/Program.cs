using HtmlParser.HtmlLoaderService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium.Chrome;
using ProductPriceStatistics.Core.Repositories;
using ProductPriceStatistics.Domain.Services;
using ProductPriceStatistics.Infrastructure.EFCoreRepository;

namespace ProductPriceStatistics.ScraperWorkerService
{
    public class Program
    {
        private static DbContextOptions<ProductPriceStatisticsDbContext> dbOptions;

        public static void Main(string[] args)
        {
            //var capabilities = new DesiredCapabilities();
            //capabilities.SetCapability(CapabilityType.BrowserName, "chrome");
            //capabilities.SetCapability(CapabilityType.BrowserVersion, "87.0");
            //var driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities);
            //var htmlLoaderService = new SeleniumHtmlLoaderService(driver);
            dbOptions = new DbContextOptionsBuilder<ProductPriceStatisticsDbContext>()
                .UseNpgsql("Host=192.168.0.131;Port=5432;Database=New_Product_DB;Username=admin;Password=admin")
                .Options;

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton(typeof(IAddPriceToProductService), typeof(AddPriceToProductService));
                    services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
                    services.AddSingleton(typeof(IPriceRepository), typeof(PriceRepository));
                    services.AddSingleton<ProductPriceStatisticsDbContext>(new ProductPriceStatisticsDbContext(dbOptions));
                    services.AddSingleton<IHtmlLoaderService>(new SeleniumHtmlLoaderService(new ChromeDriver(@"C:\WebDriver")));
                });
    }
}
