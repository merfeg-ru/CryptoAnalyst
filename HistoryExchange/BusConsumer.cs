using AutoMapper;
using CommonData.BusModels;
using CommonData.Enums;
using HistoryExchange.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HistoryExchange
{
    public class BusConsumer : IConsumer<IBusPairTradeInfoMessage>
    {
        private readonly ILogger<BusConsumer> _logger;
        private readonly IHistoryService _historyService;
        private readonly IMapper _mapper;

        public BusConsumer(ILogger<BusConsumer> logger, IHistoryService historyService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<IBusPairTradeInfoMessage> context)
        {
            try
            {
                // Запись в БД
                var itemShort = _mapper.Map<IList<IBusPairTradeInfo>, IList<HistoryItemShort>>(context.Message.PairsTradeInfo);
                var exchange = (Exchange)Enum.Parse(typeof(Exchange), context.Message.Exchange.ToString());

                await _historyService.AddHistoryAsync(itemShort, exchange, CancellationToken.None);
                _logger.LogInformation($"Данные записаны");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при попытке получить данные из шины и записать в БД");
            }

        }
    }
}
