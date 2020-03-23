namespace MatchingEngine.AdminApi.Models.Candles
{
    /// <summary>
    /// Specifies a candle status.
    /// </summary>
    public enum CandleType
    {
        /// <summary>
        /// Unspecified candle type.
        /// </summary>
        None,

        /// <summary>
        /// Indicates one-minute time frame. 
        /// </summary>
        Minute,

        /// <summary>
        /// Indicates one-hour time frame. 
        /// </summary>
        Hour,

        /// <summary>
        /// Indicates one-day time frame. 
        /// </summary>
        Day,

        /// <summary>
        /// Indicates one-month time frame. 
        /// </summary>
        Month
    }
}
