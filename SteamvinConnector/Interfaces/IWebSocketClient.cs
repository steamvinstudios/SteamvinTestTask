using System;
using System.Threading.Tasks;
using BitfinexConnector.Core.Models;

namespace BitfinexConnector.Core.Interfaces
{
    public interface IWebSocketClient
    {
        /// <summary>
        /// Событие при получении данных о трейде.
        /// </summary>
        event Action<Trade> OnTradeReceived;

        /// <summary>
        /// Событие при получении данных о свече.
        /// </summary>
        event Action<Candle> OnCandleReceived;

        /// <summary>
        /// Установление соединения.
        /// </summary>
        Task ConnectAsync();

        /// <summary>
        /// Подписка на трейды для указанного символа.
        /// </summary>
        Task SubscribeToTradesAsync(string symbol);

        /// <summary>
        /// Подписка на свечи для указанного символа и таймфрейма.
        /// </summary>
        Task SubscribeToCandlesAsync(string symbol, string timeframe);
    }
}
