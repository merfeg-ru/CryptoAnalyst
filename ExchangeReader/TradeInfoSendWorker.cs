using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeReader
{
    public class TradeInfoSendWorker : BackgroundService
    {
        // Значение частоты опроса по умолчанию
        private readonly TimeSpan _requestsDelay = TimeSpan.FromSeconds(20);

        private readonly IBusSenderService _busSenderService;
        private readonly IExchangeReaderService _exchangeReaderService;
        private readonly ILogger<TradeInfoSendWorker> _logger;
        private readonly IConfiguration _configuration;

        public TradeInfoSendWorker(IBusSenderService busSenderService, IExchangeReaderService exchangeReaderService, 
            ILogger<TradeInfoSendWorker> logger, IConfiguration configuration)
        {
            _busSenderService = busSenderService ?? throw new ArgumentNullException(nameof(busSenderService));
            _exchangeReaderService = exchangeReaderService ?? throw new ArgumentNullException(nameof(exchangeReaderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _requestsDelay = TimeSpan.FromSeconds(int.Parse(
                _configuration.GetSection("Exchanges")["IntervalRequestsSecond"]));
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning($"TradeInfoSendWorker запущен");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var tradeInfo = await _exchangeReaderService.GetTradeInfoAsync();
                    var resultSending = await _busSenderService.Send(tradeInfo, stoppingToken);

                    if (!resultSending)
                        _logger.LogWarning("Ошибка отправки данных");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка в работе TradeInfoSendWorker");
                }

                // Ожидание
                await Task.Delay(_requestsDelay, stoppingToken);
            }
        }
    }
}
