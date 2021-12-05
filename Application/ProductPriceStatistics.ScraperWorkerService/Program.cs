using HtmlParser.HtmlLoaderService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductPriceStatistics.Infrastructure.RabbitMQService;
using ProductPriceStatistics.ScraperWorkerService.Configurations;
using Serilog;
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

                    Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()                     
                        .WriteTo.MongoDBBson("mongodb://192.168.0.171:27017/logs")
                        .WriteTo.Console()
                        .CreateLogger();
                    services.AddLogging(loggingBuilder =>
                    loggingBuilder.AddSerilog(dispose: true));


                    var parserConfiguration = new List<ParserConfiguration>();
                    configuration.GetSection(ParserConfiguration.ConfigurationKey).Bind(parserConfiguration);
                    var htmlParserConfigurations = ParserConfigurationFactory.CreteParserConfiguration(parserConfiguration);
                    services.AddSingleton(htmlParserConfigurations);

                    var parserTimeIntervalConfiguration = new ParserTimeIntervalConfiguration();
                    configuration.GetSection(ParserTimeIntervalConfiguration.ConfigurationKey).Bind(parserTimeIntervalConfiguration);
                    services.AddSingleton(parserTimeIntervalConfiguration);

                    var rabbitMQServiceConfiguration = new RabbitMQServiceConfiguration();
                    configuration.GetSection(RabbitMQServiceConfiguration.ConfigurationKey).Bind(rabbitMQServiceConfiguration);
                    services.AddSingleton(rabbitMQServiceConfiguration);

                    services.AddHostedService<Worker>();
                    services.AddSingleton<IHtmlLoaderService, PlayWrightHtmlLoaderService>();
                });
    }
}
