using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitfinexConnector.Core.Interfaces;
using BitfinexConnector.Core.Models;

namespace BitfinexConnector.Core.Clients
{
    public class BitfinexConnector : ITestConnector
    {
        private readonly IRestClient _restClient;
        private readonly IWebSocketClient _webSocketClient;

        public BitfinexConnector(IRestClient restClient, IWebSocketClient webSocketClient)
        {
            _restClient = restClient;
            _webSocketClient = webSocketClient;
        }

        #region Rest

        public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
        {
            // Пример адаптера: получаем трейды за последние 30 минут и возвращаем maxCount записей
            DateTime end = DateTime.UtcNow;
            DateTime start = end.AddMinutes(-30);
            var trades = await _restClient.GetTradesAsync(pair, start, end);
            return trades.Take(maxCount);
        }

        public Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
        {
            // Простейшая реализация – преобразуем periodInSec в таймфрейм (например, "1m" для 60 секунд)
            string timeframe = periodInSec == 60 ? "1m" : $"{periodInSec}s";
            DateTime start = from.HasValue ? from.Value.UtcDateTime : DateTime.UtcNow.AddHours(-1);
            DateTime end = to.HasValue ? to.Value.UtcDateTime : DateTime.UtcNow;
            return _restClient.GetCandlesAsync(pair, timeframe, start, end);
        }

        public Task<Ticker> GetTickerAsync(string pair)
        {
            return _restClient.GetTickerAsync(pair);
        }

        #endregion

        #region Socket

        public event Action<Trade> NewBuyTrade
        {
            add { _webSocketClient.OnTradeReceived += value; }
            remove { _webSocketClient.OnTradeReceived -= value; }
        }

        // Для демонстрации делегируем событие для продаж (можно доработать логику фильтрации)
        public event Action<Trade> NewSellTrade;

        public void SubscribeTrades(string pair, int maxCount = 100)
        {
            // Пример вызова метода подписки в WebSocket-клиенте
            _webSocketClient.SubscribeToTradesAsync(pair);
        }

        public void UnsubscribeTrades(string pair)
        {
            // Реализация отписки (не реализовано в данном примере)
            throw new NotImplementedException("Отписка от трейдов не реализована.");
        }

        public event Action<Candle> CandleSeriesProcessing
        {
            add { _webSocketClient.OnCandleReceived += value; }
            remove { _webSocketClient.OnCandleReceived -= value; }
        }

        public void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
        {
            string timeframe = periodInSec == 60 ? "1m" : $"{periodInSec}s";
            _webSocketClient.SubscribeToCandlesAsync(pair, timeframe);
        }

        public void UnsubscribeCandles(string pair)
        {
            throw new NotImplementedException("Отписка от свечей не реализована.");
        }

        #endregion
    }
}
