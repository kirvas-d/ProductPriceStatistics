using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using HtmlParser.HtmlLoaderService;
using ProductPriceStatistics.Domain.Services;

namespace ProductPriceStatistics.ScraperWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ScraperService.ScraperService _scraperService;
        private readonly IAddPriceToProductService _addPriceToProductService;

        public Worker(ILogger<Worker> logger, IHtmlLoaderService htmlLoaderService, IAddPriceToProductService addPriceToProductService)
        {
            _logger = logger;
            _scraperService = new ScraperService.ScraperService(htmlLoaderService, new ParserCongiguration().Configuration);
            _addPriceToProductService = addPriceToProductService;
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


                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
