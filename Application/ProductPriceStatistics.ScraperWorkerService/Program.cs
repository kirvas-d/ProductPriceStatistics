using HtmlParser.HtmlLoaderService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using ProductPriceStatistics.Core.CommandHandlers;
using ProductPriceStatistics.Core.Commands;
using ProductPriceStatistics.Core.Repositories;
using ProductPriceStatistics.Infrastructure.EFCoreRepository;
using ProductPriceStatistics.ScraperWorkerService.Configurations;
using System;
using System.Collections.Generic;

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
                    
                    var dbContextConfiguration = new DbContextConfiguration();
                    configuration.GetSection(DbContextConfiguration.ConfigurationKey).Bind(dbContextConfiguration);
                    var dbOptions = new DbContextOptionsBuilder<ProductPriceStatisticsDbContext>()
                        .UseNpgsql(dbContextConfiguration.ConnectionString)
                        .Options;

                    var htmlLoaderServiceConfiguration = new HtmlLoaderServiceConfiguration();
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

                    var parserConfiguration = new List<ParserConfiguration>();
                    configuration.GetSection(ParserConfiguration.ConfigurationKey).Bind(parserConfiguration);
                    var htmlParserConfigurations = ParserConfigurationFactory.CreteParserConfiguration(parserConfiguration);

                    var parserTimeIntervalConfiguration = new ParserTimeIntervalConfiguration();
                    configuration.GetSection(ParserTimeIntervalConfiguration.ConfigurationKey).Bind(parserTimeIntervalConfiguration);

                    services.AddHostedService<Worker>();
                    services.AddSingleton(typeof(ICommandHandler<AddPriceToProductCommand>), typeof(AddPriceToProductCommandHandler));
                    services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
                    services.AddSingleton(typeof(IPriceRepository), typeof(PriceRepository));
                    services.AddSingleton<ProductPriceStatisticsDbContext>(new ProductPriceStatisticsDbContext(dbOptions));
                    services.AddSingleton<IHtmlLoaderService>(new SeleniumHtmlLoaderService(webDriver));
                    services.AddSingleton(htmlParserConfigurations);
                    services.AddSingleton(parserTimeIntervalConfiguration);
                });
    }
}
