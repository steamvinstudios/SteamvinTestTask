using BitfinexConnector.Core.Interfaces;
using BitfinexConnector.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexConnector.Core.Clients
{
    class BitfinexWebSocketClient : IWebSocketClient
    {
        private int _connectionCount = 0;
        private DateTime _lastConnectionTime = DateTime.MinValue;

        public event Action<Trade> OnTradeReceived;
        public event Action<Candle> OnCandleReceived;

        public async Task ConnectAsync(bool isAuthenticated = false)
        {
            // обработка ограничений на подключения
            if (isAuthenticated && _connectionCount >= 5)
                throw new RateLimitException("Max 5 authenticated connections per 15s");

            var baseUrl = isAuthenticated ? "wss://api.bitfinex.com/ws/2" : "wss://api-pub.bitfinex.com/ws/2";
            await _webSocket.ConnectAsync(new Uri(baseUrl), CancellationToken.None);

            // обновление счетчик подключений
            _connectionCount++;
            _lastConnectionTime = DateTime.UtcNow;
        }

        public Task ConnectAsync()
        {
            throw new NotImplementedException();
        }

        public Task SubscribeToCandlesAsync(string symbol, string timeframe)
        {
            throw new NotImplementedException();
        }

        // обработка деривативов
        public Task SubscribeToDerivatives(string symbol)
        {
            var msg = $"{{\"event\":\"subscribe\", \"channel\":\"trades\", \"symbol\":\"{symbol}\"}}";
            return SendWebSocketMessage(msg);
        }

        public Task SubscribeToTradesAsync(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
