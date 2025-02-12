using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitfinexConnector.Core.Interfaces;
using BitfinexConnector.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BitfinexConnector.Core.Clients.WebSocket
{
    public class BitfinexWebSocketClient : IWebSocketClient
    {
        private ClientWebSocket _webSocket;
        private CancellationTokenSource _cts;
        private const string WSUrl = "wss://api-pub.bitfinex.com/ws/2";

        public event Action<Trade> OnTradeReceived;
        public event Action<Candle> OnCandleReceived;

        public BitfinexWebSocketClient()
        {
            _webSocket = new ClientWebSocket();
            _cts = new CancellationTokenSource();
        }

        public async Task ConnectAsync()
        {
            await _webSocket.ConnectAsync(new Uri(WSUrl), CancellationToken.None);
            _ = ReceiveLoop();
        }

        private async Task ReceiveLoop()
        {
            var buffer = new byte[8192];

            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    string json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    ProcessMessage(json);
                }
            }
        }

        private void ProcessMessage(string json)
        {
            // Простейшая обработка – если получен массив, трактуем как трейд
            var token = JToken.Parse(json);
            if (token.Type == JTokenType.Array)
            {
                var trade = new Trade
                {
                    Pair = "tBTCUSD", // Пример: можно доработать определение символа
                    Price = token[0].Value<decimal>(),
                    Amount = token[1].Value<decimal>(),
                    Side = token[1].Value<decimal>() > 0 ? "buy" : "sell",
                    Time = DateTimeOffset.UtcNow,
                    Id = Guid.NewGuid().ToString()
                };

                OnTradeReceived?.Invoke(trade);
            }
            // Дополнительно можно добавить обработку свечей и других типов сообщений
        }

        public async Task SubscribeToTradesAsync(string symbol)
        {
            var subscribeMessage = new
            {
                @event = "subscribe",
                channel = "trades",
                symbol = symbol,
                prec = "P0",
                freq = "F0",
                len = 100
            };
            string json = JsonConvert.SerializeObject(subscribeMessage);
            await SendMessageAsync(json);
        }

        public async Task SubscribeToCandlesAsync(string symbol, string timeframe)
        {
            var subscribeMessage = new
            {
                @event = "subscribe",
                channel = "candles",
                key = $"trade:{timeframe}:{symbol}"
            };
            string json = JsonConvert.SerializeObject(subscribeMessage);
            await SendMessageAsync(json);
        }

        private async Task SendMessageAsync(string message)
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _cts.Token);
            }
        }
    }
}
