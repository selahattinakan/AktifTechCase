using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.MessageBroker.Services
{
    public class RabbitMQClient : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "OrderDirectExchange";
        public static string RoutingName = "order-route";
        public static string QueueName = "queue-order";


        public RabbitMQClient(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, type: ExchangeType.Direct, true, false);

            _channel.QueueDeclare(QueueName, true, false, false);

            _channel.QueueBind(QueueName, ExchangeName, RoutingName);

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
