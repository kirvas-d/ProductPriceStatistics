using Microsoft.Extensions.Hosting;
using ProductStoreMicroservice.Models;
using RabbitMQService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStoreMicroservice.Services
{
    public class ProductReceivingService : IHostedService
    {
        private readonly RabbitMQMicroService<ProductMeasure> rabbitMQService;
        private readonly RabbitMQMicroServiceConfigure Configure;
        private readonly IProductReceivingHandler<ProductMeasure> receivingHandler;

        public ProductReceivingService(RabbitMQMicroServiceConfigure configure, IProductReceivingHandler<ProductMeasure> productReceivingHandler) 
        {
            Configure = configure;
            rabbitMQService = new RabbitMQMicroService<ProductMeasure>(Configure);
            receivingHandler = productReceivingHandler;
            rabbitMQService.Received += RabbitMQService_Received;
        }

        private void RabbitMQService_Received(object sender, DeliverEventArgs<ProductMeasure> e)
        {
            
            receivingHandler.Handle(e.Message);
            Console.WriteLine($"Обработано сообщение {e.Message.Name}-{e.Message.StoreName}");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            rabbitMQService.StarConsume();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            rabbitMQService.Dispose();
            return Task.CompletedTask;
        }
    }
}
