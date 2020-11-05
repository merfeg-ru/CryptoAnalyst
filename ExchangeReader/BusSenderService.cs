using CommonData.BusModels;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeReader
{
    public class BusSenderService : IBusSenderService
    {
        private readonly ILogger<BusSenderService> _logger;
        private readonly IPublishEndpoint _endpoint;

        public BusSenderService(ILogger<BusSenderService> logger, IPublishEndpoint endpoint)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
        }

        public async Task<bool> Send(IList<IBusPairTradeInfo> tradeInfoList, CancellationToken cancellationToken)
        {
            try
            {
                var tradeInfoMessage = new BusPairTradeInfoMessage()
                {
                    MessageId = Guid.NewGuid(),
                    PairsTradeInfo = tradeInfoList,
                    CreationDate = DateTime.Now
                };

                await _endpoint.Publish<IBusPairTradeInfoMessage>(tradeInfoMessage, cancellationToken);
                _logger.LogWarning($"Передача в шину данных выполнена [{tradeInfoMessage}]");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
