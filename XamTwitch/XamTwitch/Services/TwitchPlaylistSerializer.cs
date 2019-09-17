using System;
using Microsoft.MobCAT.Converters;

namespace XamTwitch.Services
{
    public class TwitchPlaylistSerializer : ISerializer<string>
    {
        public string MediaType => "application/*";

        public T Deserialize<T>(string value)
        {
            throw new NotSupportedException();
        }

        public string Serialize<T>(T value)
        {
            throw new NotSupportedException();
        }
    }
}
