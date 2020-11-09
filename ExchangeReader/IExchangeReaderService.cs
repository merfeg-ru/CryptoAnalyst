using CommonData.BusModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeReader
{
    public interface IExchangeReaderService
    {
        string ExchangeName { get; }
        Task<IList<IBusPairTradeInfo>> GetTradeInfoAsync();
        (int OkRequests, int BadRequests) GetStatistic();
    }
}
