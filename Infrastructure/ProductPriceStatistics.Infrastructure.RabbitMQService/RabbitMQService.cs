using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ProductPriceStatistics.Infrastructure.RabbitMQService
{
    public class RabbitMQService<TMessage> : IDisposable
    {
        private IConnection connection;
        private IModel channel;
        private EventingBasicConsumer consumer;

        public RabbitMQServiceConfiguration Configure { get; init; }

        public event EventHandler<DeliverEventArgs<TMessage>> Received; 

        public RabbitMQService(RabbitMQServiceConfiguration configure) 
        {
            if (configure == null)
            {
                throw new NullReferenceException();
            }
            Configure = configure;

            var factory = new ConnectionFactory() { HostName = Configure.HostName, Port = Configure.Port };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: Configure.QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
          
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var json = Encoding.UTF8.GetString(e.Body.ToArray());
            TMessage message = JsonConvert.DeserializeObject<TMessage>(json);

            Received?.Invoke(this, new DeliverEventArgs<TMessage>() { Message = message });
        }

        public void PublishMessage(TMessage message)
        {
            if (message == null) 
            {
                throw new NullReferenceException();
            }

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                                 routingKey: Configure.QueueName,
                                 basicProperties: null,
                                 body: body);
        }

        public void StarConsume() 
        {
            consumer = new EventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            channel.BasicConsume(queue: Configure.QueueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Dispose()
        {
            channel.Dispose();
            connection.Dispose();
        }
    }
}
