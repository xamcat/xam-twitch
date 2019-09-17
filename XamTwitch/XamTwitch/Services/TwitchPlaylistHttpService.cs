using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.MobCAT.Services;
using XamTwitch.Helpers;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public class TwitchPlaylistHttpService : BaseHttpService, ITwitchPlaylistHttpService
    {
        public TwitchPlaylistHttpService() : base(Constants.TwitchPlaylistApiUri, handler: null)
        {
            Serializer = new TwitchPlaylistSerializer();
        }
        
        public async Task<string> GetPlaylistUriAsync(string userName, TwitchToken token)
        {
            if (string.IsNullOrWhiteSpace(userName) || token == null ||  string.IsNullOrWhiteSpace(token.Sig) || string.IsNullOrWhiteSpace(token.Token))
                throw new InvalidOperationException();

            var channelName = userName.ToLower();
            var tokenUpdated = Uri.EscapeUriString(token.Token);
            System.Diagnostics.Debug.WriteLine($"Original: {token.Token}\nUpdated: {tokenUpdated}");

            var playlistUrl = $"api/channel/hls/{channelName}.m3u8?sig={token.Sig}&token={tokenUpdated}";
            System.Diagnostics.Debug.WriteLine(playlistUrl);

            var playlistReponseMessage = await base.SendWithRetryAsync(HttpMethod.Get, playlistUrl).ConfigureAwait(false);
            var playlistReponse = await playlistReponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var match = Regex.Match(playlistReponse, @"(?<stream>https://(.+).m3u8)");
            if (!match.Success)
                throw new InvalidOperationException();

            var streamUrl = match.Groups["stream"].Value;
            return streamUrl;
        }
    }
}
