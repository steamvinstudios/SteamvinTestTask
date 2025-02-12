using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using BitfinexConnector.Core.Interfaces;
using BitfinexConnector.Core.Models;
using Newtonsoft.Json.Linq;

namespace BitfinexConnector.Core.Clients.Rest
{
    public class BitfinexRestClient : IRestClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api-pub.bitfinex.com/v2/";

        public BitfinexRestClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Trade>> GetTradesAsync(string symbol, DateTime start, DateTime end)
        {
            // Пример URL: https://api-pub.bitfinex.com/v2/trades/{symbol}/hist?start={start}&end={end}
            long startMs = new DateTimeOffset(start).ToUnixTimeMilliseconds();
            long endMs = new DateTimeOffset(end).ToUnixTimeMilliseconds();
            string url = $"{BaseUrl}trades/{symbol}/hist?start={startMs}&end={endMs}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();

            var jsonArray = JArray.Parse(content);
            var trades = new List<Trade>();

            foreach (var item in jsonArray)
            {
                // Формат массива: [ID, MTS, AMOUNT, PRICE]
                trades.Add(new Trade
                {
                    Id = item[0].ToString(),
                    Time = DateTimeOffset.FromUnixTimeMilliseconds(item[1].Value<long>()),
                    Amount = item[2].Value<decimal>(),
                    Price = item[3].Value<decimal>(),
                    Pair = symbol,
                    Side = item[2].Value<decimal>() > 0 ? "buy" : "sell"
                });
            }

            return trades;
        }

        public async Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string timeframe, DateTime start, DateTime end)
        {
            long startMs = new DateTimeOffset(start).ToUnixTimeMilliseconds();
            long endMs = new DateTimeOffset(end).ToUnixTimeMilliseconds();
            string url = $"{BaseUrl}candles/trade:{timeframe}:{symbol}/hist?start={startMs}&end={endMs}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();

            var jsonArray = JArray.Parse(content);
            var candles = new List<Candle>();

            foreach (var item in jsonArray)
            {
                // Формат массива: [MTS, OPEN, CLOSE, HIGH, LOW, VOLUME]
                candles.Add(new Candle
                {
                    OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(item[0].Value<long>()),
                    OpenPrice = item[1].Value<decimal>(),
                    ClosePrice = item[2].Value<decimal>(),
                    HighPrice = item[3].Value<decimal>(),
                    LowPrice = item[4].Value<decimal>(),
                    TotalVolume = item[5].Value<decimal>(),
                    Pair = symbol
                });
            }

            return candles;
        }

        public async Task<Ticker> GetTickerAsync(string symbol)
        {
            // Пример URL: https://api-pub.bitfinex.com/v2/ticker/{symbol}
            string url = $"{BaseUrl}ticker/{symbol}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();

            // Формат ответа: [ BID, BID_SIZE, ASK, ASK_SIZE, DAILY_CHANGE, DAILY_CHANGE_PERC, LAST_PRICE, VOLUME, HIGH, LOW ]
            var jsonArray = JArray.Parse(content);

            return new Ticker
            {
                Pair = symbol,
                Bid = jsonArray[0].Value<decimal>(),
                Ask = jsonArray[2].Value<decimal>(),
                LastPrice = jsonArray[6].Value<decimal>(),
                Volume = jsonArray[7].Value<decimal>()
            };
        }
    }
}
