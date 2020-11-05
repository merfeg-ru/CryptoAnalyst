using CommonData.BusModels;
using CryptoExchange;
using CryptoExchange.Enums;
using CryptoExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeReader
{
    public class ExchangeReaderService : IExchangeReaderService
    {
        private readonly ICryptoExchangeService _exchangeService;
        private static Dictionary<string, PairDetail> _pairDetailsDictionary;

        public ExchangeReaderService(ICryptoExchangeService exchangeService)
        {
            _exchangeService = exchangeService ?? throw new ArgumentNullException(nameof(exchangeService));
        }

        public async Task<IList<IBusPairTradeInfo>> GetTradeInfoAsync()
        {
            /* Нужно как то позаботится о переодическом обновлении _pairDetailsDictionary,
               валюта может перестать торговаться, или появится новая */

            if (_pairDetailsDictionary == null)
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
                        QuoteCurrency = pairDetails.QuoteCurrency
                    });
                }
            }

            return returnList;
        }

        private async Task InitPairDetailsDictionaryAsync()
        {
            _pairDetailsDictionary = new Dictionary<string, PairDetail>();

            var pairDetails = await _exchangeService.GetPairDetailsAsync();

            foreach (var pair in pairDetails.Where(_ => _.PairStatus == PairStatus.Active))
            {
                _pairDetailsDictionary.Add(pair.Name, pair);
            }
        }
    }
}
