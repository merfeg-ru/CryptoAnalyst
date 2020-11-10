using CommonData.Enums;
using HistoryExchange.Context;
using HistoryExchange.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HistoryExchange
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly HistoryContext _context;

        public HistoryRepository(HistoryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddHistoryAsync(IList<HistoryItemDTO> items, Exchange exchange, CancellationToken cancellationToken)
        {
            if (exchange == Exchange.Binance)
            {
                await _context.HistoryBinance.AddRangeAsync(items.Cast<HistoryBinanceDTO>(), cancellationToken);
            }
            else
            {
                throw new ArgumentException(nameof(exchange));
            }
            
        }

        public IQueryable<HistoryItemDTO> GetHistory(Exchange exchange)
        {
            if (exchange == Exchange.Binance)
            {
                return _context.HistoryBinance.AsNoTracking();
            }
            else
            {
                throw new ArgumentException(nameof(exchange));
            }
        }
    }
}
