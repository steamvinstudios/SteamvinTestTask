using BitfinexConnector.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexConnector.Core.Interfaces
{
    interface IRestClient
    {
        Task<IEnumerable<Trade>> GetTradesAsync(string symbol, DateTime start, DateTime end);
        Task<IEnumerable<Candle>> GetCandlesAsync(string symbol, string timeframe, DateTime start, DateTime end);
        Task<Ticker> GetTickerAsync(string symbol);
    }
}
