using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData.BusModels
{
    public interface IBusPairTradeInfo
    {
        string Name { get; set; }
        string BaseCurrency { get; set; }
        string QuoteCurrency { get; set; }
        decimal AskPrice { get; set; }
        decimal BidPrice { get; set; }
        decimal Volume { get; set; }
    }
}
