using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using BitfinexConnector.Core.Clients.Rest;
using BitfinexConnector.Core.Models;

namespace BitfinexConnector.Tests
{
    public class BitfinexRestClientTests
    {
        [Fact]
        public async Task GetTradesAsync_ReturnsTrades()
        {
            // Arrange
            var client = new BitfinexRestClient();
            DateTime end = DateTime.UtcNow;
            DateTime start = end.AddHours(-1);

            // Act
            var trades = await client.GetTradesAsync("tBTCUSD", start, end);

            // Assert
            Assert.NotNull(trades);
            Assert.NotEmpty(trades);
            // Можно добавить дополнительные проверки, например, что все трейды имеют положительный ID или корректное время
            Assert.All(trades, trade =>
            {
                Assert.False(string.IsNullOrWhiteSpace(trade.Id));
                Assert.True(trade.Time > DateTimeOffset.MinValue);
            });
        }

        [Fact]
        public async Task GetTickerAsync_ReturnsValidTicker()
        {
            // Arrange
            var client = new BitfinexRestClient();

            // Act
            Ticker ticker = await client.GetTickerAsync("tBTCUSD");

            // Assert
            Assert.NotNull(ticker);
            Assert.Equal("tBTCUSD", ticker.Pair);
            Assert.True(ticker.LastPrice > 0, "LastPrice should be greater than zero.");
        }

        [Fact]
        public async Task GetCandlesAsync_ReturnsCandles()
        {
            // Arrange
            var client = new BitfinexRestClient();
            DateTime end = DateTime.UtcNow;
            DateTime start = end.AddHours(-1);

            // Act
            var candles = await client.GetCandlesAsync("tBTCUSD", "1m", start, end);

            // Assert
            Assert.NotNull(candles);
            Assert.NotEmpty(candles);
            Assert.All(candles, candle =>
            {
                Assert.True(candle.OpenPrice >= 0);
                Assert.True(candle.ClosePrice >= 0);
            });
        }
    }
}
