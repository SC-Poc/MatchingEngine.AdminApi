using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MatchingEngine.AdminApi.Models.OrderBooks
{
    /// <summary>
    /// Represents a limit order details.
    /// </summary>
    public class LimitOrderModel
    {
        /// <summary>
        /// The identifier of the limit order.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The limit order price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The limit order volume.
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// The wallet identifier.
        /// </summary>
        public string WalletId { get; set; }

        /// <summary>
        /// The limit order type.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LimitOrderType Type { get; set; }
    }
}
