using HtmlParser.HtmlLoaderService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using ProductPriceStatistics.Core.Repositories;
using ProductPriceStatistics.Domain.Services;
using ProductPriceStatistics.Infrastructure.EFCoreRepository;
using ProductPriceStatistics.ScraperWorkerService.Configurations;
using System;

namespace ProductPriceStatistics.ScraperWorkerService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    
                    DbContextConfiguration dbContextConfiguration = new DbContextConfiguration();
                    configuration.GetSection(DbContextConfiguration.ConfigurationKey).Bind(dbContextConfiguration);
                    var dbOptions = new DbContextOptionsBuilder<ProductPriceStatisticsDbContext>()
                        .UseNpgsql(dbContextConfiguration.ConnectionString)
                        .Options;

                    HtmlLoaderServiceConfiguration htmlLoaderServiceConfiguration = new HtmlLoaderServiceConfiguration();
                    configuration.GetSection(HtmlLoaderServiceConfiguration.ConfigurationKey).Bind(htmlLoaderServiceConfiguration);
                    IWebDriver webDriver = null;
                    if (htmlLoaderServiceConfiguration.TypeDriver == TypeDriver.Local) 
                    {
                        webDriver = new ChromeDriver(htmlLoaderServiceConfiguration.PathToDriver);
                    }
                    if (htmlLoaderServiceConfiguration.TypeDriver == TypeDriver.Remote) 
                    {
                        var chromeOptions = new ChromeOptions();
                        webDriver = new RemoteWebDriver(new Uri(htmlLoaderServiceConfiguration.PathToDriver), chromeOptions);
                    }

                    services.AddHostedService<Worker>();
                    services.AddSingleton(typeof(IAddPriceToProductService), typeof(AddPriceToProductService));
                    services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
                    services.AddSingleton(typeof(IPriceRepository), typeof(PriceRepository));
                    services.AddSingleton<ProductPriceStatisticsDbContext>(new ProductPriceStatisticsDbContext(dbOptions));
                    services.AddSingleton<IHtmlLoaderService>(new SeleniumHtmlLoaderService(webDriver));
                });
    }
}
