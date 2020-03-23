using System.Collections.Generic;

namespace MatchingEngine.AdminApi.Models
{
    /// <summary>
    /// Represents paged response
    /// </summary>
    public class PagedResponse<T>
    {
        /// <summary>
        /// A collection of items.
        /// </summary>
        public IReadOnlyList<T> Items { get; set; }

        /// <summary>
        /// The total count of items.
        /// </summary>
        public int Total { get; set; }
    }
}
