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
using ProductPriceStatistics.Infrastructure.RabbitMQService;
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
                                    
                    var parserConfiguration = new List<ParserConfiguration>();
                    configuration.GetSection(ParserConfiguration.ConfigurationKey).Bind(parserConfiguration);
                    var htmlParserConfigurations = ParserConfigurationFactory.CreteParserConfiguration(parserConfiguration);

                    var parserTimeIntervalConfiguration = new ParserTimeIntervalConfiguration();
                    configuration.GetSection(ParserTimeIntervalConfiguration.ConfigurationKey).Bind(parserTimeIntervalConfiguration);

                    var rabbitMQServiceConfiguration = new RabbitMQServiceConfiguration();
                    configuration.GetSection(RabbitMQServiceConfiguration.ConfigurationKey).Bind(rabbitMQServiceConfiguration);

                    services.AddHostedService<Worker>();
                    services.AddSingleton<IHtmlLoaderService ,PlayWrightHtmlLoaderService>();
                    services.AddSingleton(htmlParserConfigurations);
                    services.AddSingleton(parserTimeIntervalConfiguration);
                    services.AddSingleton(rabbitMQServiceConfiguration);
                });
    }
}
