using System;

namespace MatchingEngine.AdminApi.Models.Assets
{
    /// <summary>
    /// Represents an asset.
    /// </summary>
    public class AssetModel
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The human-readable name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The additional information.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The number of decimal places.
        /// </summary>
        public int Accuracy { get; set; }

        /// <summary>
        /// Indicates that the asset is disabled.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// The creation date and time.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// The last update date and time.
        /// </summary>
        public DateTime Modified { get; set; }
    }
}
