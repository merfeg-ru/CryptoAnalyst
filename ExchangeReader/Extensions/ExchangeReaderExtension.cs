using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeReader.Extensions
{
    public static class ExchangeReaderExtension
    {
        public static IServiceCollection RegisterDataBusServices(this IServiceCollection services, IConfiguration section)
        {
            var busSettings = section.GetSection("BusSettings");
            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(busSettings["HostName"], busSettings["VirtualHost"],
                    h => {
                        h.Username(busSettings["UserName"]);
                        h.Password(busSettings["Password"]);
                    });

                cfg.ExchangeType = ExchangeType.Direct;
            }));
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            return services;
        }
    }
}
