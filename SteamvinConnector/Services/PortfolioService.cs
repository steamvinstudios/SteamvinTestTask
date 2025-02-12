using BitfinexConnector.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexConnector.Core.Services
{
    public class PortfolioService
    {
        readonly IRestClient _restClient;
        private Dictionary<string, decimal> _assets = new Dictionary<string, decimal>
        {
            ["BTC"] = 1m,
            ["XRP"] = 15000m,
            ["XMR"] = 50m,
            ["DASH"] = 30m
        };

        PortfolioService(IRestClient restClient) => _restClient = restClient;

        public async Task<Dictionary<string, decimal>> CalculatePortfolioInAllCurrenciesAsync()
        {
            var results = new Dictionary<string, decimal>();
            foreach (var targetCurrency in new[] { "USDT", "BTC", "XRP", "XMR", "DASH" })
            {
                decimal total = 0;
                foreach (var (currency, amount) in _assets)
                    total += await ConvertAsset(currency, amount, targetCurrency);
                results[targetCurrency] = total;
            }
            return results;
        }

        private async Task<decimal> FetchConversionRate(string from, string to)
        {
            // для маржинальных валют используются символы с 'f'
            var symbol = from.StartsWith("f") || to.StartsWith("f")
                ? $"f{from}{to}"
                : $"t{from}{to}";

            var ticker = await _restClient.GetTickerAsync(symbol);
            return ticker.LastPrice;
        }

        private async Task<decimal> ConvertAsset(string from, decimal amount, string to)
        {
            if (from == to) return amount;
            var rate = await FetchConversionRate(from, to);
            return amount * rate;
        }

        private async Task<decimal> FetchConversionRate(string from, string to)
        {

        }
    }
}
