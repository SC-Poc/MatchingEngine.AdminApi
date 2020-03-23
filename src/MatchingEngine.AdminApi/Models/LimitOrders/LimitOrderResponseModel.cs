using System;
using MatchingEngine.Client.Models.Trading;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MatchingEngine.AdminApi.Models.LimitOrders
{
    /// <summary>
    /// Represents limit order creation response.
    /// </summary>
    public class LimitOrderResponseModel
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The limit order status.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LimitOrderStatus Status { get; set; }

        /// <summary>
        /// The error status message.
        /// </summary>
        public string Reason { get; set; }
    }
}
