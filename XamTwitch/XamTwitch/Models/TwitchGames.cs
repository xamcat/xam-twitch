using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace XamTwitch.Models
{
    public class TwitchGames
    {
        [JsonProperty("data")]
        public List<TwitchGame> Games { get; set; }
    }
}
