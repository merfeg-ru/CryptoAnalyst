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
        // частоту опроса брать из настроек
        private const int ThreadDelay = 20 * 1000;

        private readonly IBusSenderService _busSenderService;
        private readonly IExchangeReaderService _exchangeReaderService;
        private readonly ILogger<TradeInfoSendWorker> _logger;

        public TradeInfoSendWorker(IBusSenderService busSenderService, IExchangeReaderService exchangeReaderService, ILogger<TradeInfoSendWorker> logger)
        {
            _busSenderService = busSenderService ?? throw new ArgumentNullException(nameof(busSenderService));
            _exchangeReaderService = exchangeReaderService ?? throw new ArgumentNullException(nameof(exchangeReaderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
                await Task.Delay(ThreadDelay, stoppingToken);
            }
        }
    }
}
