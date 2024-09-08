using AktifTech.WorkerService;
using AktifTech.WorkerService.Services;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton(sp => new ConnectionFactory() { HostName = "localhost", Port = 5672, DispatchConsumersAsync = true });//appsettings'den de alýnabilir
builder.Services.AddSingleton<RabbitMQClientService>();
builder.Services.AddSingleton<WriteTextService>();

var host = builder.Build();
host.Run();
