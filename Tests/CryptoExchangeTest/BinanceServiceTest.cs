﻿using CryptoExchange;
using CryptoExchange.Exchanges.Binance;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CryptoExchangeTest
{
    public class BinanceServiceTest
    {
        [Fact]
        public async Task GetPairDetailsStatsAsyncTest()
        {

            var service = new BinanceService(A.Fake<ICryptoExchangeRepository>());
            await service.GetPairDetailsAsync();
            await service.GetPairsStatsAsync();

            var newService = new BinanceService(A.Fake<ICryptoExchangeRepository>());
            var result = newService.GetStatistic();

            Assert.True((result.BadRequests + result.OkRequests) == 2);
        }

        [Fact]
        public async Task GetOkBadStatisticTest()
        {
            var repository = A.Fake<ICryptoExchangeRepository>();
            A.CallTo(() => repository.GetPairDetailsAsync()).Throws(() => new Exception());

            var service = new BinanceService(repository);
            await service.GetPairDetailsAsync();
            await service.GetPairDetailsAsync();

            var result = service.GetStatistic();

            Assert.Equal(2, result.BadRequests);
        }
    }
}
