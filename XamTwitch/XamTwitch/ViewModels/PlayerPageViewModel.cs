using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.MobCAT.MVVM;
using Newtonsoft.Json;
using Xamarin.Essentials;
using XamTwitch.Models;

namespace XamTwitch.ViewModels
{
    public class PlayerPageViewModel : BaseNavigationViewModel
    {
        private TwitchStream _stream;
        public TwitchStream Stream
        {
            get => _stream;
            set
            {
                if(RaiseAndUpdate(ref _stream, value))
                {
                    FetchTwitchStreamAsync();
                }
            }
        }

        private string _streamSource;
        public string StreamSource
        {
            get { return _streamSource; }
            set { RaiseAndUpdate(ref _streamSource, value); }
        }

        public PlayerPageViewModel()
        {
        }

        private async Task FetchTwitchStreamAsync()
        {
            try
            {
                if (Stream == null)
                    return;

                System.Diagnostics.Debug.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, IsMain: {MainThread.IsMainThread}");
                var channelName = Stream.UserName.ToLower();
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
                if(!match.Success)
                    throw new InvalidOperationException();

                var streamUrl = match.Groups["stream"].Value;

                System.Diagnostics.Debug.WriteLine($"{Thread.CurrentThread.ManagedThreadId}, IsMain: {MainThread.IsMainThread}");
                // need UI
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    StreamSource = streamUrl;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
