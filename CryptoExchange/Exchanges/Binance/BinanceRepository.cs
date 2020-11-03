﻿using Binance.NetCore;
using CryptoExchange.Enums;
using CryptoExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Exchanges.Binance
{
    public class BinanceRepository : ICryptoExchangeRepository
    {
        private readonly BinanceApiClient _apiClient;

        public BinanceRepository()
        {
            _apiClient = new BinanceApiClient();
        }

        public async Task<IList<PairDetail>> GetPairDetailsAsync()
        {
            var apiItems = await _apiClient.GetTradingPairDetailsAsync();

            return apiItems.Select(_ => new PairDetail
                {
                    Name = _.symbol,
                    BaseCurrency = _.baseAsset,
                    QuoteCurrency = _.quoteAsset,
                    PairStatus = (_.status == "TRADING") ? PairStatus.Active : PairStatus.NoActive
                }).ToList();
        }

        public async Task<IList<PairTradeInfo>> GetPairsStatsAsync()
        {
            var apiItems = await _apiClient.Get24HourStatsAsync();

            return apiItems.Select(_ => new PairTradeInfo
            {
                Name = _.symbol,
                AskPrice = decimal.Parse(_.askPrice),
                BidPrice = decimal.Parse(_.bidPrice),
                Volume = decimal.Parse(_.volume)
            }).ToList();
        }
    }
}
