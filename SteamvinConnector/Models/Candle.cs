using System;

namespace BitfinexConnector.Core.Models
{
    public class Candle
    {
        /// <summary>
        /// Валютная пара.
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Цена открытия.
        /// </summary>
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// Максимальная цена.
        /// </summary>
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Минимальная цена.
        /// </summary>
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Цена закрытия.
        /// </summary>
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// Общая сумма сделок.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Общий объем.
        /// </summary>
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// Время открытия свечи.
        /// </summary>
        public DateTimeOffset OpenTime { get; set; }
    }
}
