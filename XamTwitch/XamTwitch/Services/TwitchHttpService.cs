using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.MobCAT.Services;
using XamTwitch.Helpers;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public class TwitchHttpService : BaseHttpService, ITwitchHttpService
    {
        private const string ClientIDHeaderKey = "Client-ID";

        public TwitchHttpService() : base(Constants.TwitchApiUri, handler: null)
        {
            SetDefaultRequestHeaders(shouldClear: false,
                headers: new KeyValuePair<string, string>(ClientIDHeaderKey, Constants.TwitchApiKey));
            Serializer = new NewtonsoftJsonSerializer();
        }

        public Task<TwitchGames> GetTwitchGamesAsync(string gameName)
        {
            return GetAsync<TwitchGames>($"games?name={gameName}");
        }

        public Task<TwitchStreams> GetTwitchStreamsAsync()
        {
            return GetAsync<TwitchStreams>($"streams");
        }
    }
}
