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

        public async Task<string> GetTwitchStreamUrlAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            System.Diagnostics.Debug.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, IsMain: {MainThread.IsMainThread}");
            var channelName = userName.ToLower();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Client-ID", Constants.TwitchApiKey);
            var tokenUrl = $"https://api.twitch.tv/api/channels/{channelName}/access_token";
            var tokenResponse = await httpClient.GetStringAsync(tokenUrl).ConfigureAwait(false);
            var token = JsonConvert.DeserializeObject<TwitchToken>(tokenResponse);

            if (string.IsNullOrWhiteSpace(token.Sig) || string.IsNullOrWhiteSpace(token.Token))
                throw new InvalidOperationException();

            var tokenUpdated = Uri.EscapeUriString(token.Token);
            System.Diagnostics.Debug.WriteLine($"Original: {token.Token}\nUpdated: {tokenUpdated}");

            var playlistUrl = $"https://usher.ttvnw.net/api/channel/hls/{channelName}.m3u8?sig={token.Sig}&token={tokenUpdated}";
            System.Diagnostics.Debug.WriteLine(playlistUrl);

            var playlistReponse = await httpClient.GetStringAsync(playlistUrl).ConfigureAwait(false);

            var match = Regex.Match(playlistReponse, @"(?<stream>https://(.+).m3u8)");
            if (!match.Success)
                throw new InvalidOperationException();

            var streamUrl = match.Groups["stream"].Value;
            return streamUrl;
        }
    }
}
