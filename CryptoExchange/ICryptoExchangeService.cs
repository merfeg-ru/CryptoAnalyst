using CryptoExchange.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange
{
    public interface ICryptoExchangeService
    {
        /// <summary>
        /// Получить имя биржи
        /// </summary>
        string Name { get; }

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

        /// <summary>
        /// Получить статистику успешных и не успешных запросов по бирже
        /// </summary>
        /// <returns></returns>
        (int OkRequests, int BadRequests) GetStatistic();
    }
}
