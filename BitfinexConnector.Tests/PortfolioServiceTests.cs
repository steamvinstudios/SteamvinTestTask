using System.Threading.Tasks;
using Xunit;
using BitfinexConnector.Core.Services;

namespace BitfinexConnector.Tests
{
    public class PortfolioServiceTests
    {
        [Fact]
        public async Task CalculatePortfolioAsync_ReturnsPortfolioDictionary()
        {
            // Arrange
            var service = new PortfolioService();

            // Act
            var portfolio = await service.CalculatePortfolioAsync();

            // Assert
            Assert.NotNull(portfolio);
            Assert.NotEmpty(portfolio);
            // Проверим, что для каждой целевой валюты (USDT, BTC, XRP, XMR, DASH) получено значение
            Assert.Contains("USDT", portfolio.Keys);
            Assert.Contains("BTC", portfolio.Keys);
            Assert.Contains("XRP", portfolio.Keys);
            Assert.Contains("XMR", portfolio.Keys);
            Assert.Contains("DASH", portfolio.Keys);
        }
    }
}
