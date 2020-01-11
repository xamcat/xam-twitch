using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.MobCAT.Services;
using XamTwitch.Helpers;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public class TwitchAnonymousHttpService : BaseHttpService, ITwitchAnonymousHttpService
    {
        public TwitchAnonymousHttpService() : base(Constants.TwitchApiUri, handler: null)
        {
            SetDefaultRequestHeaders(shouldClear: false,
                headers: new KeyValuePair<string, string>(Constants.ClientIDHeaderKey, Constants.TwitchAnonymousClientId));
            Serializer = new NewtonsoftJsonSerializer();
        }

        public async Task<TwitchToken> GetTwitchTokenAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException(nameof(userName));
            }

            var channelName = userName.ToLower();
            var url = $"api/channels/{channelName}/access_token";
            var token = await GetAsync<TwitchToken>(url);
            return token;
        }
    }
}
