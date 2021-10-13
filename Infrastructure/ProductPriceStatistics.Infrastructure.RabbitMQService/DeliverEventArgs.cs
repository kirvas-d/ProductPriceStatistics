using System;

namespace ProductPriceStatistics.Infrastructure.RabbitMQService
{
    public class DeliverEventArgs<TMessage>: EventArgs
    {
        public TMessage Message { get; set; }
    }
}
