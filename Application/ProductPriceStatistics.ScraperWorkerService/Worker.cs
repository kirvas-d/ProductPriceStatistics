using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using HtmlParser.HtmlLoaderService;
using ProductPriceStatistics.Domain.Services;
using ProductPriceStatistics.ScraperWorkerService.Configurations;
using HtmlParser;
using System.Collections.Generic;

namespace ProductPriceStatistics.ScraperWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ScraperService.ScraperService _scraperService;
        private readonly IAddPriceToProductService _addPriceToProductService;
        private readonly ParserTimeIntervalConfiguration _parserTimeIntervalConfiguration;

        public Worker(ILogger<Worker> logger, IHtmlLoaderService htmlLoaderService, IAddPriceToProductService addPriceToProductService, IEnumerable<PageHtmlParserConfiguration> pageHtmlParserConfiguration, ParserTimeIntervalConfiguration parserTimeIntervalConfiguration)
        {
            _logger = logger;
            _scraperService = new ScraperService.ScraperService(htmlLoaderService, pageHtmlParserConfiguration);
            _addPriceToProductService = addPriceToProductService;
            _parserTimeIntervalConfiguration = parserTimeIntervalConfiguration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                foreach (var productMeassure in _scraperService.ScrapeProducts()) 
                {
                    _addPriceToProductService.AddPriceToProduct(productMeassure.ProductName, productMeassure.Price, productMeassure.StoreName, DateTime.Now);
                }

                await Task.Delay(_parserTimeIntervalConfiguration.IntervalTimeSpan, stoppingToken);             
            }
        }
    }
}
