using Newtonsoft.Json;

namespace XamTwitch.Models
{
    public class Pagination
    {
        [JsonProperty("cursor")]
        public string Cursor { get; set; }
    }
}