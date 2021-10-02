using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using HtmlParser.HtmlLoaderService;
using ProductPriceStatistics.ScraperWorkerService.Configurations;
using HtmlParser;
using System.Collections.Generic;
using ProductPriceStatistics.Core.CommandHandlers;
using ProductPriceStatistics.Core.Commands;

namespace ProductPriceStatistics.ScraperWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ScraperService.ScraperService _scraperService;
        private readonly ICommandHandler<AddPriceToProductCommand> _addPriceToProductCommandHandler;
        private readonly ParserTimeIntervalConfiguration _parserTimeIntervalConfiguration;

        public Worker(
            ILogger<Worker> logger, 
            IHtmlLoaderService htmlLoaderService, 
            ICommandHandler<AddPriceToProductCommand> 
            addPriceToProductCommandHandler, 
            IEnumerable<PageHtmlParserConfiguration> pageHtmlParserConfiguration, 
            ParserTimeIntervalConfiguration parserTimeIntervalConfiguration)
        {
            _logger = logger;
            _scraperService = new ScraperService.ScraperService(htmlLoaderService, pageHtmlParserConfiguration);
            _addPriceToProductCommandHandler = addPriceToProductCommandHandler;
            _parserTimeIntervalConfiguration = parserTimeIntervalConfiguration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                foreach (var productMeassure in _scraperService.ScrapeProducts()) 
                {
                    _addPriceToProductCommandHandler.Handle(new AddPriceToProductCommand(
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
