using CommonData.BusModels;
using CommonData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeReader
{
    public interface IExchangeReaderService
    {
        Exchange Exchange { get; }
        Task<IList<IBusPairTradeInfo>> GetTradeInfoAsync();
        (int OkRequests, int BadRequests) GetStatistic();
    }
}
