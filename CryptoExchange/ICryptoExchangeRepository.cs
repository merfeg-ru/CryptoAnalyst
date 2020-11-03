using CryptoExchange.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoExchange
{
    public interface ICryptoExchangeRepository
    {
        /// <summary>
        /// Получить данные о торгуемых парах: цена, объем
        /// </summary>
        /// <returns></returns>
        Task<IList<PairTradeInfo>> GetPairsStatsAsync();

        /// <summary>
        /// Получить данные о торгуемых парах: название валют, фильтры, статус
        /// </summary>
        /// <returns></returns>
        Task<IList<PairDetail>> GetPairDetailsAsync();
    }
}
