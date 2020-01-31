using System;
using Newtonsoft.Json;

namespace XamTwitch.Models
{
    public class TwitchChannelFollow
    {
        [JsonProperty("from_id")]
        public string FromId { get; set; }

        [JsonProperty("from_name")]
        public string FromName { get; set; }

        [JsonProperty("to_id")]
        public string ToId { get; set; }

        [JsonProperty("to_name")]
        public string ToName { get; set; }

        [JsonProperty("followed_at")]
        public DateTime FollowedAt { get; set; }
    }
}