using AktifTech.Business.Interfaces;
using AktifTech.MessageBroker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Business.Services
{
    public class RabbitMQPublisherService : IRabbitMQPublishService
    {
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public RabbitMQPublisherService(RabbitMQPublisher rabbitMQPublisher)
        {
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public void Publish(string message)
        {
            _rabbitMQPublisher.Publish(message);
        }
    }
}
