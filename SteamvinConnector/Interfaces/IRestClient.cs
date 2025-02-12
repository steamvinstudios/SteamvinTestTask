using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitfinexConnector.Core.Models;

namespace BitfinexConnector.Core.Interfaces
{
    public interface IRestClient
    {
        /// <summary>
        /// Получение трейдов за указанный интервал времени.
        /// </summary>
        Task<IEnumerable<Trade>> GetTradesAsync(string symbol, DateTime start, DateTime end);

        /// <summary>
        /// Получение серии свечей за указанный интервал времени.
        /// </summary>
        Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string timeframe, DateTime start, DateTime end);

        /// <summary>
        /// Получение информации о тикере.
        /// </summary>
        Task<Ticker> GetTickerAsync(string symbol);
    }
}
