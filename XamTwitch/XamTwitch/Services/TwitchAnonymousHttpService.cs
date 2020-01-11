using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.MobCAT.Services;
using XamTwitch.Helpers;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public class TwitchAnonymousHttpService : BaseHttpService, ITwitchAnonymousHttpService
    {
        private string _anonymousClientId = null;

        public TwitchAnonymousHttpService() : base(null, handler: null)
        {
            Serializer = new NewtonsoftJsonSerializer();
        }

        public async Task<TwitchToken> GetTwitchTokenAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            await SetAnonymousClientId(userName);

            var channelName = userName.ToLower();
            var url = $"{Constants.TwitchApiUri}api/channels/{channelName}/access_token";
            var token = await GetAsync<TwitchToken>(url);
            return token;
        }

        private async Task SetAnonymousClientId(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            if (!string.IsNullOrWhiteSpace(_anonymousClientId))
            {
                return;
            }

            var playerHtmlUrl = $"https://player.twitch.tv/?channel={userName}";
            var playerResponse = await GetAsync<string>(playerHtmlUrl, deserializeResponse: false);
            var playerJS = Regex.Match(playerResponse, @"src=""(?<playerJS>js/video.\w+.js)""").Groups["playerJS"].Value;
            var playerJSUrl = $"https://player.twitch.tv/{playerJS}";
            var playerJSResponse = await GetAsync<string>(playerJSUrl, deserializeResponse: false);
            _anonymousClientId = Regex.Match(playerJSResponse, @"{""Client-ID"":""(?<clientId>[^""]+)""").Groups["clientId"].Value;
            SetDefaultRequestHeaders(shouldClear: true, headers: new KeyValuePair<string, string>(Constants.ClientIDHeaderKey, _anonymousClientId));

            System.Diagnostics.Debug.WriteLine($"Anonymous client id fetched and set: {_anonymousClientId}");
        }
    }
}
