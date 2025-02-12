using BitfinexConnector.Core.Interfaces;
using BitfinexConnector.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexConnector.Core.Clients
{
    public class BitfinexConnector : ITestConnector
    {
        private readonly IRestClient _restClient;
        private readonly IWebSocketClient _webSocketClient;

        public event Action<Trade> NewSellTrade;
        public event Action<Candle> CandleSeriesProcessing;

        public BitfinexConnector(IRestClient restClient, IWebSocketClient webSocketClient)
        {
            _restClient = restClient;
            _webSocketClient = webSocketClient;
        }

        // Реализация методов ITestConnector
        public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
        {
            return await _restClient.GetTradesAsync(pair, maxCount);
        }

        public Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
        {
            throw new NotImplementedException();
        }

        public void SubscribeTrades(string pair, int maxCount = 100)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeTrades(string pair)
        {
            throw new NotImplementedException();
        }

        public void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeCandles(string pair)
        {
            throw new NotImplementedException();
        }

        // Подписка на события WebSocket
        public event Action<Trade> NewBuyTrade
        {
            add => _webSocketClient.NewBuyTrade += value;
            remove => _webSocketClient.NewBuyTrade -= value;
        }
    }
}
