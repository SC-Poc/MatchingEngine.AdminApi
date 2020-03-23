using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MatchingEngine.AdminApi.Models.IsAlive
{
    public class IsAliveModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("env")]
        public string Env { get; set; }

        [JsonProperty("host")]
        public string HostName { get; set; }

        [JsonProperty("isDebug")]
        public bool IsDebug { get; set; }

        [JsonProperty("startedAt")]
        public DateTime StartedAt { get; set; }

        [JsonProperty("issueIndicators")]
        public List<StateIndicator> StateIndicators { get; set; }

        public class StateIndicator
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }
    }
}
