using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryExchange.Extensions
{
    public static class HistoryExchangeExtension
    {
        public static IServiceCollection RegisterDataBusServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Bus
            services.AddMassTransit(c =>
            {
                c.AddConsumer<BusConsumer>();
            });

            var busSettings = configuration.GetSection("BusSettings");
            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(busSettings["HostName"], busSettings["VirtualHost"],
                    h =>
                    {
                        h.Username(busSettings["UserName"]);
                        h.Password(busSettings["Password"]);
                    });
            }));

            return services;
        }
    }
}
