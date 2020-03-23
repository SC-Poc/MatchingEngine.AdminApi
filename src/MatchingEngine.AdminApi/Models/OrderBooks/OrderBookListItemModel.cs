using System;

namespace MatchingEngine.AdminApi.Models.OrderBooks
{
    /// <summary>
    /// Represents an order book list item.
    /// </summary>
    public class OrderBookListItemModel
    {
        /// <summary>
        /// The asset pair identifier.
        /// </summary>
        public string AssetPairId { get; set; }

        /// <summary>
        /// Best sell price.
        /// </summary>
        public decimal? Ask { get; set; }
        
        /// <summary>
        /// Best buy price.
        /// </summary>
        public decimal? Bid { get; set; }
        
        /// <summary>
        /// Mid price.
        /// </summary>
        public decimal? Mid { get; set; }
        
        /// <summary>
        /// Spread.
        /// </summary>
        public decimal? Spread { get; set; }

        /// <summary>
        /// The number of sell limit order.
        /// </summary>
        public int SellOrdersCount { get; set; }

        /// <summary>
        /// The number of buy limit order.
        /// </summary>
        public int BuyOrdersCount { get; set; }
        
        /// <summary>
        /// The date and time of creation.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
