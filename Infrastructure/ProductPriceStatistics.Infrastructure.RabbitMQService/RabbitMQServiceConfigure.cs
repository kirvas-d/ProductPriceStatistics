﻿namespace ProductPriceStatistics.Infrastructure.RabbitMQService
{
    public class RabbitMQServiceConfiguration
    {
        public const string ConfigurationKey = "RabbitMQService";

        public string HostName { get; set; }
        public int Port { get; set; }
        public string QueueName { get; set; }
    }
}
