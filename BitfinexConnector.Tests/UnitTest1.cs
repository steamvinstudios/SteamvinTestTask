using BitfinexConnector.Core.Clients;

namespace BitfinexConnector.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public async Task GetTrades_ReturnsData()
        {
            var client = new BitfinexRestClient();
            var trades = await client.GetTradesAsync("tBTCUSD", DateTime.UtcNow.AddHours(-1), DateTime.UtcNow);
            Assert.NotEmpty(trades);
        }
    }
}
