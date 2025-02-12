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
        private ClientWebSocket _webSocket = new ClientWebSocket();

        public event Action<Trade> OnTradeReceived;
        public event Action<Candle> OnCandleReceived;

        public async Task ConnectAsync()
        {
            await _webSocket.ConnectAsync(new Uri("wss://api.bitfinex.com/ws/2"), CancellationToken.None);
            await ListenForMessages();
        }

        public Task SubscribeToCandlesAsync(string symbol, string timeframe)
        {
            throw new NotImplementedException();
        }

        public Task SubscribeToTradesAsync(string symbol)
        {
            throw new NotImplementedException();
        }

        private async Task ListenForMessages()
        {
            var buffer = new byte[1024];
            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                ProcessMessage(message);
            }
        }

        private void ProcessMessage(string message)
        {

        }
    }
}
