using System;
namespace XamTwitch.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class TwitchStreams
    {
        [JsonProperty("data")]
        public List<TwitchStream> Data { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
    }

    public class TwitchStream
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("viewer_count")]
        public string ViewerCount { get; set; }

        [JsonProperty("started_at")]
        public DateTimeOffset StartedAt { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("tag_ids")]
        public List<Guid> TagIds { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("cursor")]
        public string Cursor { get; set; }
    }

    public enum TypeEnum { Live };
}


