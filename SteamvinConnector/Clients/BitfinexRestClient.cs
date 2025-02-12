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
    class BitfinexRestClient : IRestClient
    {
        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.bitfinex.com/v2/")
        };

        public async Task<IEnumerable<Trade>> GetTradesAsync(string symbol, DateTime start, DateTime end)
        {
            var endpoint = $"trades/{symbol}/hist?start={ToUnixMs(start)}&end={ToUnixMs(end)}";
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
