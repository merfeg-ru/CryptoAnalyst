using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonData.BusModels;
using ExchangeReader.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExchangeReader.Controllers
{
    [ApiController]
    [Route("")]
    public class StatisticController : ControllerBase
    {
        private readonly IExchangeReaderService _exchangeReader;
        private readonly IBusSenderService _busSenderService;
        public StatisticController(IExchangeReaderService exchangeReader, IBusSenderService busSenderService)
        {
            _exchangeReader = exchangeReader ?? throw new Exception(nameof(exchangeReader));
            _busSenderService = busSenderService ?? throw new Exception(nameof(busSenderService));
        }

        [HttpGet]
        [Route("statistic")]
        public async Task<StatisticView> GetStatistic(CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return new StatisticView
                {
                    ExchangeRequests = _exchangeReader.GetStatistic(),
                    ExchangeName = _exchangeReader.ExchangeName,
                    BusSendingMessage = _busSenderService.GetCountSendingItems()
                };
            }, cancellationToken);
        }

        [HttpGet]
        [Route("lastdata")]
        public async Task<IList<IBusPairTradeInfo>> GetLastData(CancellationToken cancellationToken)
        {
            return await Task.Run(() => _busSenderService.GetLastSendingData(), cancellationToken);
        }
    }
}
