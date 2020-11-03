using CryptoExchange.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.Models
{
    public class PairDetail
    {
        /// <summary>
        /// Имя пары
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Валюта которую покупают
        /// </summary>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Валюта за которую покупают
        /// </summary>
        public string QuoteCurrency { get; set; }

        /// <summary>
        /// Статус торговли парой, активно/не активно
        /// </summary>
        public PairStatus PairStatus { get; set; }
    }
}
