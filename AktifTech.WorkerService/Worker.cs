using AktifTech.WorkerService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data;
using System.Text.Json;
using System.Text;
using System.Threading.Channels;

namespace AktifTech.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQClientService _rabbitmqClientService;
        private readonly WriteTextService _writeTextService;
        private IModel _channel;

        public Worker(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, WriteTextService writeTextService)
        {
            _logger = logger;
            _rabbitmqClientService = rabbitMQClientService;
            _writeTextService = writeTextService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitmqClientService.Connect();
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var consumer = new AsyncEventingBasicConsumer(_channel);
                _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
                consumer.Received += Consumer_Received;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kuyruk tan�ml� de�il, l�tfen �nce sipari� onaylay�n�z."); //bu durumdan kurtulmak i�in api program.cs dosyas�nda uygulama aya�a kalk�nca RabbitMQClientService s�n�f�n�n Connect metodu �a�r�labilir.
            }
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var orderMessage = JsonSerializer.Deserialize<string>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            _writeTextService.WriteText(orderMessage);
            Console.WriteLine(orderMessage);
            _channel.BasicAck(@event.DeliveryTag, false);
        }
    }
}
