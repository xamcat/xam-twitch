using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamTwitch.Models
{
    public class TwitchChannelFollows
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("data")]
        public IList<TwitchChannelFollow> Follows { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}