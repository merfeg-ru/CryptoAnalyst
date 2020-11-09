using CommonData.BusModels;
using CryptoExchange;
using CryptoExchange.Enums;
using CryptoExchange.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeReader
{
    public class ExchangeReaderService : IExchangeReaderService, IDisposable
    {
        private readonly ICryptoExchangeService _exchangeService;
        private readonly IConfiguration _configuration;

        private Dictionary<string, PairDetail> _pairDetailsDictionary = new Dictionary<string, PairDetail>();
        private readonly Timer _timer;

        public string ExchangeName => _exchangeService.Name;

        public ExchangeReaderService(ICryptoExchangeService exchangeService, IConfiguration configuration)
        {
            _exchangeService = exchangeService ?? throw new ArgumentNullException(nameof(exchangeService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            var updatePairDetailsTimeMinute = int.Parse(
                _configuration.GetSection("Exchanges")["UpdatePairDetailsTimeMinute"]);

            // Таймер обновления PairDetails, на случай появления новых валютных пар, или исчезновения старых
            _timer = new Timer(async (_) =>
            {
                await InitPairDetailsDictionaryAsync();
            }, null, TimeSpan.FromMinutes(updatePairDetailsTimeMinute), TimeSpan.FromMinutes(updatePairDetailsTimeMinute));
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public async Task<IList<IBusPairTradeInfo>> GetTradeInfoAsync()
        {
            if (_pairDetailsDictionary.Count == 0)
                await InitPairDetailsDictionaryAsync();

            var pairTradeInfo = await _exchangeService.GetPairsStatsAsync();

            var returnList = new List<IBusPairTradeInfo>();
            foreach (var pair in pairTradeInfo)
            {
                if (_pairDetailsDictionary.TryGetValue(pair.Name, out var pairDetails))
                {
                    returnList.Add(new BusPairTradeInfo
                    {
                        Name = pair.Name,
                        AskPrice = pair.AskPrice,
                        BidPrice = pair.BidPrice,
                        Volume = pair.Volume,
                        BaseCurrency = pairDetails.BaseCurrency,
                        QuoteCurrency = pairDetails.QuoteCurrency,
                        ExchangeName = _exchangeService.Name
                    });
                }
            }

            return returnList;
        }

        public (int OkRequests, int BadRequests) GetStatistic()
        {
            return _exchangeService.GetStatistic();
        }

        private async Task InitPairDetailsDictionaryAsync()
        {
            var pairDetails = await _exchangeService.GetPairDetailsAsync();

            var newDetails = new Dictionary<string, PairDetail>();
            foreach (var pair in pairDetails.Where(_ => _.PairStatus == PairStatus.Active))
            {
                newDetails.Add(pair.Name, pair);
            }

            _pairDetailsDictionary = newDetails;
        }
    }
}
