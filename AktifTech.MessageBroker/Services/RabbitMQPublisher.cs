using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AktifTech.MessageBroker.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClient _rabbitmqClientService;

        public RabbitMQPublisher(RabbitMQClient rabbitmqClientService)
        {
            _rabbitmqClientService = rabbitmqClientService;
        }

        public void Publish(string message)
        {
            var channel = _rabbitmqClientService.Connect();

            var bodyString = JsonSerializer.Serialize(message);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(RabbitMQClient.ExchangeName, RabbitMQClient.RoutingName, properties, bodyByte);
        }
    }
}
