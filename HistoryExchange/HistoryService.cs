using AutoMapper;
using CommonData.Enums;
using HistoryExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HistoryExchange
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _repository;
        private readonly IMapper _mapper;

        public HistoryService(IHistoryRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task AddHistoryAsync(IList<HistoryItemShort> items, Exchange exchange, CancellationToken cancellationToken)
        {
            IList<HistoryItemDTO> dtoItems;

            if (exchange == Exchange.Binance)
            {
                dtoItems = _mapper.Map<IList<HistoryItemShort>, IList<HistoryBinanceDTO>>(items)
                    .Cast<HistoryItemDTO>().ToList();
            }
            else
            {
                throw new ArgumentException(nameof(exchange));
            }

            await _repository.AddHistoryAsync(dtoItems, exchange, cancellationToken);
        }

        public Task<IList<HistoryItemDTO>> GetHistoryAsync(Exchange exchange, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<HistoryItemDTO>> GetHistoryAsync(Exchange exchange, DateTime dateFrom, DateTime dateTo, TimeSpan discrete, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
