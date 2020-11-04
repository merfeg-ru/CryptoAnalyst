using CryptoExchange.Exchanges.Binance;
using System.Threading.Tasks;
using Xunit;

namespace CryptoExchangeTest
{
    public class BinanceRepositoryTest
    {
        [Fact]
        public async Task GetPairDetailsAsyncTest()
        {
            var repository = new BinanceRepository();
            var result = await repository.GetPairDetailsAsync();

            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task GetPairsStatsAsyncTest()
        {
            var repository = new BinanceRepository();
            var result = await repository.GetPairsStatsAsync();

            Assert.True(result.Count > 0);
        }
    }
}
