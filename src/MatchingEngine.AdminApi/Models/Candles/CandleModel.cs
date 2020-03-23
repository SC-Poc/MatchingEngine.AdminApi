using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MatchingEngine.AdminApi.Models.Candles
{
    /// <summary>
    /// Represents a candle.
    /// </summary>
    public class CandleModel
    {
        /// <summary>
        /// The type.
        /// </summary>
        public string AssetPairId { get; set; }

        /// <summary>
        /// The date and time of open price.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// The type.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public CandleType Type { get; set; }

        /// <summary>
        /// The open price of period.
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// The close price of period.
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// The high price of period.
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// The low price of period.
        /// </summary>
        public double Low { get; set; }
    }
}
