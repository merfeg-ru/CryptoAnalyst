using CryptoExchange.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoExchange.Exchanges.Binance
{
    public class BinanceService : ICryptoExchangeService
    {
        private readonly ICryptoExchangeRepository _repository;
        private static int _okRequests = 0;
        private static int _badRequests = 0;

        public BinanceService(ICryptoExchangeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public string Name => "Binance";

        public async Task<IList<PairDetail>> GetPairDetailsAsync()
        {
            IList<PairDetail> result = new List<PairDetail>();
            try
            {
                result = await _repository.GetPairDetailsAsync();
                Interlocked.Increment(ref _okRequests);
            }
            catch
            {
                Interlocked.Increment(ref _badRequests);
            }

            return result;
        }

        public async Task<IList<PairTradeInfo>> GetPairsStatsAsync()
        {
            IList<PairTradeInfo> result = new List<PairTradeInfo>();
            try
            {
                result = await _repository.GetPairsStatsAsync();
                Interlocked.Increment(ref _okRequests);
            }
            catch
            {
                Interlocked.Increment(ref _badRequests);
            }

            return result;
        }

        public (int OkRequests, int BadRequests) GetStatistic()
        {
            return (_okRequests, _badRequests);
        }
    }
}
