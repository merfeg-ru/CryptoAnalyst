using CommonData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeReader.Models
{
    public class StatisticView
    {
        public Exchange Exchange { get; set; }
        public (int OkRequests, int BadRequests) ExchangeRequests { get; set; }
        public int BusSendingMessage { get; set; }
    }
}
