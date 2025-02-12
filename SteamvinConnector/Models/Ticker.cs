namespace BitfinexConnector.Core.Models
{
    public class Ticker
    {
        /// <summary>
        /// Валютная пара (например, tBTCUSD).
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Последняя цена.
        /// </summary>
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Лучшее предложение на покупку (bid).
        /// </summary>
        public decimal Bid { get; set; }

        /// <summary>
        /// Лучшее предложение на продажу (ask).
        /// </summary>
        public decimal Ask { get; set; }

        /// <summary>
        /// Объем торгов за 24 часа.
        /// </summary>
        public decimal Volume { get; set; }
    }
}
