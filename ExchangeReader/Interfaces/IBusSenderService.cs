using CommonData.BusModels;
using CommonData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeReader
{
    public interface IBusSenderService
    {
        Task<bool> Send(IList<IBusPairTradeInfo> tradeInfo, Exchange exchange, CancellationToken cancellationToken);
        int GetCountSendingItems();
        IList<IBusPairTradeInfo> GetLastSendingData();
    }
}
