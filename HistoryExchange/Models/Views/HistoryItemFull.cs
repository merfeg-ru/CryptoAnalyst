using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryExchange.Models
{
    public class HistoryItemFull
    {
        public int Id { get; set; }
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public decimal AskPrice { get; set; }
        public decimal BidPrice { get; set; }
        public decimal Volume { get; set; }
        public HistorySlice Slice { get; set; }
    }
}
