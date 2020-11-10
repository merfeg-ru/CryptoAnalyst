using CommonData.Enums;
using HistoryExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HistoryExchange
{
    public interface IHistoryRepository
    {
        Task AddHistoryAsync(IList<HistoryItemDTO> items, Exchange exchange, CancellationToken cancellationToken);
        IQueryable<HistoryItemDTO> GetHistory(Exchange exchange);
    }
}
