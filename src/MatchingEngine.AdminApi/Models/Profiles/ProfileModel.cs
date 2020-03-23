using System;
using Newtonsoft.Json;

namespace MatchingEngine.AdminApi.Models.Profiles
{
    /// <summary>
    /// Represents a balance of an asset.
    /// </summary>
    public class ProfileModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("fullname")]
        public string FullName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
