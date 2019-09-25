using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.MobCAT.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;
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

        public async Task<TwitchToken> GetTwitchTokenAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            System.Diagnostics.Debug.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, IsMain: {MainThread.IsMainThread}");
            var channelName = userName.ToLower();
            var url = $"api/channels/{channelName}/access_token";
            var token = await GetAsync<TwitchToken>(url);
            return token;
        }
    }
}
