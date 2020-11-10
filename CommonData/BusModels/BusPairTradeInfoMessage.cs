using CommonData.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData.BusModels
{
    public class BusPairTradeInfoMessage : IBusPairTradeInfoMessage
    {
        public Guid MessageId { get; set; }
        public IList<IBusPairTradeInfo> PairsTradeInfo { get; set; }
        public DateTime CreationDate { get; set; }
        public Exchange Exchange { get; set; }

        public override string ToString()
        {
            return $"{MessageId.ToString().Substring(0, 4)}... {CreationDate} [{PairsTradeInfo.Count}]";
        }
    }
}
