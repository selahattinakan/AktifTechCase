using AktifTech.API.Authentication.Interfaces;
using AktifTech.API.Authentication.Services;
using AktifTech.Business.Decorators;
using AktifTech.Business.Interfaces;
using AktifTech.Business.Services;
using AktifTech.Cache.Extensions;
using AktifTech.Cache.Repositories;
using AktifTech.Database.DataAccessLayer;
using AktifTech.Database.Repositories;
using AktifTech.Database.Repositories.Interfaces;
using AktifTech.MessageBroker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AktifTech.API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT ile elde edilen token'ý giriniz."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddScoped<IOrderProductService, OrderProductService>();

//redis
builder.Services.AddStackExchangeRedis(builder.Configuration);
builder.Services.AddSingleton<RedisRepository>();
builder.Services.AddSingleton<IRedisService, RedisService>();

//rabbitmq
builder.Services.AddSingleton(sp => new ConnectionFactory() { HostName = builder.Configuration["RabbitMQ:Host"], Port = Int32.Parse(builder.Configuration["RabbitMQ:Port"]), DispatchConsumersAsync = true });
builder.Services.AddSingleton<RabbitMQClient>();
builder.Services.AddSingleton<RabbitMQPublisher>();
builder.Services.AddSingleton<IRabbitMQPublishService, RabbitMQPublisherService>();

//decorator design pattern
builder.Services.AddScoped<IMessageBroker>(sp =>
{
    var customerOrderService = sp.GetRequiredService<ICustomerOrderService>();
    var rabbitMQPublishService = sp.GetRequiredService<IRabbitMQPublishService>();

    var rabbitMQDecorator = new RabbitMQDecorator(customerOrderService, rabbitMQPublishService);

    return rabbitMQDecorator;
});

//JWT
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication(); //jwt
app.UseAuthorization();

app.MapControllers();

app.Run();