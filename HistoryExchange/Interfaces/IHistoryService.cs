using CommonData.Enums;
using HistoryExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HistoryExchange
{
    public interface IHistoryService
    {
        Task AddHistoryAsync(IList<HistoryItemShort> items, Exchange exchange, CancellationToken cancellationToken);
        Task<IList<HistoryItemDTO>> GetHistoryAsync(Exchange exchange, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken);
        Task<IList<HistoryItemDTO>> GetHistoryAsync(Exchange exchange, DateTime dateFrom, DateTime dateTo, TimeSpan discrete, CancellationToken cancellationToken);
    }
}
