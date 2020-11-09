using System;
using System.Collections.Generic;
using System.Text;

namespace CommonData.BusModels
{
    public class BusPairTradeInfo : IBusPairTradeInfo
    {
        public string Name { get; set; }
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public decimal AskPrice { get; set; }
        public decimal BidPrice { get; set; }
        public decimal Volume { get; set; }
        public string ExchangeName { get; set; }

        public override string ToString()
        {
            return $"{ExchangeName} {Name} A:{AskPrice} B:{BidPrice} {Volume}";
        }
    }
}
