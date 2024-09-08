using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktifTech.Cache.Extensions
{
    public static class RedisExtension
    {
        public static void AddStackExchangeRedis(this IServiceCollection services, IConfiguration configuration)
        {
            string _redisHost = configuration["Redis:Host"];
            string _redisPort = configuration["Redis:Port"];
            string _redisPassword = configuration["Redis:Password"];
            var configString = $"{_redisHost}:{_redisPort}";

            var redis = ConnectionMultiplexer.Connect(configString);
            services.AddSingleton(redis);



        }
    }
}
