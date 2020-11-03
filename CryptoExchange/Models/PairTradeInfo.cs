using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.Models
{
    public class PairTradeInfo
    {
        /// <summary>
        /// Имя пары
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена по которой продают
        /// </summary>
        public decimal AskPrice { get; set; }

        /// <summary>
        /// Цена по которой покупают
        /// </summary>
        public decimal BidPrice { get; set; }

        /// <summary>
        /// Объем торгов за 24 часа
        /// </summary>
        public decimal Volume { get; set; }
    }
}
