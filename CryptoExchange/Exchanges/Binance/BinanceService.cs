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
            IList<PairDetail> resultList = new List<PairDetail>();
            return await DoOperation(resultList, async () => await _repository.GetPairDetailsAsync());
        }

        public async Task<IList<PairTradeInfo>> GetPairsStatsAsync()
        {
            IList<PairTradeInfo> resultList = new List<PairTradeInfo>();
            return await DoOperation(resultList, async () => await _repository.GetPairsStatsAsync());
        }

        public (int OkRequests, int BadRequests) GetStatistic()
        {
            return (_okRequests, _badRequests);
        }

        private async Task<TResult> DoOperation<TResult>(TResult result, Func<Task<TResult>> func)
            where TResult: class
        {
            try
            {
                result = await func();
                Interlocked.Increment(ref _okRequests);
            }
            catch
            {
                Interlocked.Increment(ref _badRequests);
            }

            return result;
        }
    }
}
