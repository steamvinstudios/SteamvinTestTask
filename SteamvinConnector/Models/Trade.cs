using BitfinexConnector.Core.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexConnector.Core.Models
{
    public class Trade
    {
        [JsonProperty(2)]
        [JsonConverter(typeof(DecimalFormatConverter), 8)] // 8 знаков для Amount
        public decimal Amount { get; set; }

        [JsonProperty(3)]
        [JsonConverter(typeof(DecimalFormatConverter), 5)] // 5 знаков для Price
        public decimal Price { get; set; }
        // Учитываем формат массивов вместо объектов
        [JsonProperty(Order = 1)]
        public long Id { get; set; }

        [JsonProperty(Order = 2)]
        public long Timestamp { get; set; } // В миллисекундах

        [JsonProperty(Order = 3)]
        [JsonConverter(typeof(DecimalFormatConverter), 8)] // Округление до 8 знаков
        /// <summary>
        /// Валютная пара
        /// </summary>
        public string Pair { get; set; }


        /// <summary>
        /// Направление (buy/sell)
        /// </summary>
        public string Side { get; set; }

        /// <summary>
        /// Время трейда
        /// </summary>
        public DateTimeOffset Time { get; set; }


    }
}
