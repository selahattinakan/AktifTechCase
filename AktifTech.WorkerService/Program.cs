using AktifTech.WorkerService;
using AktifTech.WorkerService.Services;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton(sp => new ConnectionFactory() { HostName = builder.Configuration["RabbitMQ:Host"], Port = Int32.Parse(builder.Configuration["RabbitMQ:Port"]), DispatchConsumersAsync = true });
builder.Services.AddSingleton<RabbitMQClientService>();
builder.Services.AddSingleton<WriteTextService>();

var host = builder.Build();
host.Run();
