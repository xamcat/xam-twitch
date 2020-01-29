using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamTwitch.Models
{
    public class TwitchUserVideos
    {
        [JsonProperty("data")]
        public IList<TwitchUserVideo> Videos { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }
}
