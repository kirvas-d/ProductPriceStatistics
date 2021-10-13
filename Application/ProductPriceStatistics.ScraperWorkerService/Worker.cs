using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using HtmlParser.HtmlLoaderService;
using ProductPriceStatistics.ScraperWorkerService.Configurations;
using HtmlParser;
using System.Collections.Generic;
using ProductPriceStatistics.Core.Commands;
using ProductPriceStatistics.Infrastructure.RabbitMQService;

namespace ProductPriceStatistics.ScraperWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ScraperService.ScraperService _scraperService;
        private readonly ParserTimeIntervalConfiguration _parserTimeIntervalConfiguration;
        private readonly RabbitMQService<AddPriceToProductCommand> _rabbitMQService;

        public Worker(
            ILogger<Worker> logger, 
            IHtmlLoaderService htmlLoaderService,  
            IEnumerable<PageHtmlParserConfiguration> pageHtmlParserConfiguration, 
            ParserTimeIntervalConfiguration parserTimeIntervalConfiguration,
            RabbitMQServiceConfiguration rabbitMQMicroServiceConfigure)
        {
            _logger = logger;
            _scraperService = new ScraperService.ScraperService(htmlLoaderService, pageHtmlParserConfiguration);
            _parserTimeIntervalConfiguration = parserTimeIntervalConfiguration;
            _rabbitMQService = new RabbitMQService<AddPriceToProductCommand>(rabbitMQMicroServiceConfigure);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                foreach (var productMeassure in _scraperService.ScrapeProducts()) 
                {
                    _rabbitMQService.PublishMessage(new AddPriceToProductCommand(
                        productMeassure.ProductName,
                        productMeassure.Price,
                        productMeassure.StoreName,
                        DateTime.Now));
                }

                await Task.Delay(_parserTimeIntervalConfiguration.IntervalTimeSpan, stoppingToken);             
            }
        }
    }
}
