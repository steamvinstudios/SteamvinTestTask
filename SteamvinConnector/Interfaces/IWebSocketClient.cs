using BitfinexConnector.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexConnector.Core.Interfaces
{
    interface IWebSocketClient
    {
        event Action<Trade> OnTradeReceived;
        event Action<Candle> OnCandleReceived;
        Task ConnectAsync();
        Task SubscribeToTradesAsync(string symbol);
        Task SubscribeToCandlesAsync(string symbol, string timeframe);
    }
}
