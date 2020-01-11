using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.MobCAT.Services;
using XamTwitch.Helpers;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public class TwitchHttpService : BaseHttpService, ITwitchHttpService
    {
        public TwitchHttpService() : base(Constants.TwitchApiUri, handler: null)
        {
            SetDefaultRequestHeaders(shouldClear: false,
                headers: new KeyValuePair<string, string>(Constants.ClientIDHeaderKey, Constants.TwitchApiKey));
            Serializer = new NewtonsoftJsonSerializer();
        }

        public Task<TwitchGames> GetTwitchGamesAsync(string gameName)
        {
            return GetAsync<TwitchGames>($"helix/games?name={gameName}");
        }

        public Task<TwitchStreams> GetTwitchStreamsAsync()
        {
            return GetAsync<TwitchStreams>($"helix/streams");
        }
    }
}
