using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitfinexConnector.Core.Models;

namespace BitfinexConnector.Core.Interfaces
{
    public interface ITestConnector
    {
        #region Rest

        /// <summary>
        /// Получение новых трейдов для заданной валютной пары.
        /// </summary>
        Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount);

        /// <summary>
        /// Получение серии свечей.
        /// </summary>
        Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInSec, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0);

        /// <summary>
        /// Получение информации о тикере.
        /// </summary>
        Task<Ticker> GetTickerAsync(string pair);

        #endregion

        #region Socket

        /// <summary>
        /// Событие для получения сделок на покупку.
        /// </summary>
        event Action<Trade> NewBuyTrade;

        /// <summary>
        /// Событие для получения сделок на продажу.
        /// </summary>
        event Action<Trade> NewSellTrade;

        /// <summary>
        /// Подписка на трейды.
        /// </summary>
        void SubscribeTrades(string pair, int maxCount = 100);

        /// <summary>
        /// Отписка от трейдов.
        /// </summary>
        void UnsubscribeTrades(string pair);

        /// <summary>
        /// Событие для обработки серии свечей.
        /// </summary>
        event Action<Candle> CandleSeriesProcessing;

        /// <summary>
        /// Подписка на свечи.
        /// </summary>
        void SubscribeCandles(string pair, int periodInSec, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0);

        /// <summary>
        /// Отписка от свечей.
        /// </summary>
        void UnsubscribeCandles(string pair);

        #endregion
    }
}
