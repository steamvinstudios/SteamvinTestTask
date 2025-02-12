using BitfinexConnector.Core.Interfaces;
using BitfinexConnector.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexConnector.Core.Clients
{
    public class BitfinexRestClient : IRestClient
    {
        // базовые URL для публичных и приватных запросов
        private const string PublicBaseUrl = "https://api-pub.bitfinex.com/v2/";
        private const string PrivateBaseUrl = "https://api.bitfinex.com/v2/";

        // обработка символов
        private string NormalizeSymbol(string symbol)
        {
            if (!symbol.StartsWith("t") && !symbol.StartsWith("f"))
                return $"t{symbol}"; // По умолчанию считаем торговую пару
            return symbol;
        }

        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.bitfinex.com/v2/")
        };

        public async Task<IEnumerable<Trade>> GetTradesAsync(string symbol, DateTime start, DateTime end)
        {
            var normalizedSymbol = NormalizeSymbol(symbol);
            var endpoint = $"trades/{normalizedSymbol}/hist?start={ToUnixMs(start)}&end={ToUnixMs(end)}";
            var response = await _httpClient.GetStringAsync(endpoint);
            return JsonConvert.DeserializeObject<List<Trade>>(response);
        }

        private static long ToUnixMs(DateTime date) =>
            new DateTimeOffset(date).ToUnixTimeMilliseconds();

        public Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string timeframe, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task<Ticker> GetTickerAsync(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
