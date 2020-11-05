using CommonData.BusModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeReader
{
    public interface IExchangeReaderService
    {
        Task<IList<IBusPairTradeInfo>> GetTradeInfoAsync();
    }
}
