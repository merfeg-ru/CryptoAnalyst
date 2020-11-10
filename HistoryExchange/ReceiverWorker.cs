using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MassTransit;

namespace HistoryExchange
{
    public class ReceiverWorker : BackgroundService
    {
        private readonly ILogger<ReceiverWorker> _logger;
        private readonly IBusControl _busControl;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public ReceiverWorker(IBusControl busControl, IConfiguration configuration, IServiceProvider serviceProvider, 
            ILogger<ReceiverWorker> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                //Подписка на информацию шины данных
                var busSettings = _configuration.GetSection("BusSettings");
                var userChangeHandler = _busControl.ConnectReceiveEndpoint(busSettings["QueueName"], x =>
                {
                    x.Consumer<BusConsumer>(_serviceProvider);
                });

                await userChangeHandler.Ready;

                _logger.LogWarning("ReceiverWorker запущен!");
            }
            catch (Exception ex)
            {
                _logger.LogError("ReceiverWorker не запущен.", ex);
            }
        }
    }
}
