using CommonData.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData.BusModels
{
    public interface IBusPairTradeInfoMessage
    {
        Guid MessageId { get; set; }
        IList<IBusPairTradeInfo> PairsTradeInfo { get; set; }
        DateTime CreationDate { get; set; }
        Exchange Exchange { get; set; }
    }
}
