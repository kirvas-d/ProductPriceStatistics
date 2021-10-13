using Microsoft.Extensions.Hosting;
using ProductPriceStatistics.Core.CommandHandlers;
using ProductPriceStatistics.Core.Commands;
using ProductPriceStatistics.Infrastructure.RabbitMQService;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductPriceStatistics.WebApi.Services
{
    public class AddPriceToProductHandlerHostedService : IHostedService
    {
        private readonly RabbitMQService<AddPriceToProductCommand> _rabbitMQService;
        private readonly ICommandHandler<AddPriceToProductCommand> _addPriceToProductCommandHandler;

        public AddPriceToProductHandlerHostedService(
            RabbitMQServiceConfiguration rabbitMQServiceConfiguration,
            ICommandHandler<AddPriceToProductCommand> addPriceToProductCommandHandler) 
        {
            _rabbitMQService = new RabbitMQService<AddPriceToProductCommand>(rabbitMQServiceConfiguration);
            _rabbitMQService.Received += RabbitMQService_Received;
            _addPriceToProductCommandHandler = addPriceToProductCommandHandler;
        }

        private void RabbitMQService_Received(object sender, DeliverEventArgs<AddPriceToProductCommand> e)
        {
            _addPriceToProductCommandHandler.Handle(e.Message);
            Console.WriteLine($"Обработано сообщение {e.Message.ProductName}-{e.Message.StoreName}");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _rabbitMQService.StarConsume();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _rabbitMQService.Dispose();
            return Task.CompletedTask;
        }
    }
}
