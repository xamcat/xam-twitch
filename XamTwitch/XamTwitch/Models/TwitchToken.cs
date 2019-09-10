using System;
using Newtonsoft.Json;

namespace XamTwitch.Models
{
    public class TwitchToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("sig")]
        public string Sig { get; set; }
        [JsonProperty("mobile_restricted")]
        public bool MobileRestricted { get; set; }
        [JsonProperty("expires_at")]
        public DateTime ExpiresAt { get; set; }
    }
}
