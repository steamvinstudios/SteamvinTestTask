using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitfinexConnector.Core.Clients.Rest;
using BitfinexConnector.Core.Models;

namespace BitfinexConnector.Core.Services
{
    public class PortfolioService
    {
        // Исходный баланс портфеля
        private readonly Dictionary<string, decimal> _portfolio = new Dictionary<string, decimal>
        {
            { "BTC", 1m },
            { "XRP", 15000m },
            { "XMR", 50m },
            { "DASH", 30m }
        };

        // Целевые валюты для отображения
        private readonly string[] _targetCurrencies = new string[] { "USDT", "BTC", "XRP", "XMR", "DASH" };

        private readonly BitfinexRestClient _restClient;

        public PortfolioService()
        {
            _restClient = new BitfinexRestClient();
        }

        /// <summary>
        /// Расчет баланса портфеля в разных валютах.
        /// Для конвертации используется тикер пары, например, для BTC->USDT: tBTCUSDT.
        /// Если актив совпадает с целевой валютой, конвертация не требуется.
        /// </summary>
        public async Task<Dictionary<string, decimal>> CalculatePortfolioAsync()
        {
            var result = new Dictionary<string, decimal>();

            foreach (var target in _targetCurrencies)
            {
                decimal total = 0m;
                foreach (var asset in _portfolio)
                {
                    if (asset.Key.Equals(target, StringComparison.OrdinalIgnoreCase))
                    {
                        total += asset.Value;
                    }
                    else
                    {
                        string pair = $"t{asset.Key}{target}";
                        try
                        {
                            Ticker ticker = await _restClient.GetTickerAsync(pair);
                            total += asset.Value * ticker.LastPrice;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка получения котировки для {pair}: {ex.Message}");
                        }
                    }
                }
                result[target] = total;
            }

            return result;
        }
    }
}
