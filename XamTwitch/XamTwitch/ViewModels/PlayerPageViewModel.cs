using System;
using System.Threading.Tasks;
using Microsoft.MobCAT;
using Microsoft.MobCAT.MVVM;
using XamTwitch.Helpers;
using XamTwitch.Models;
using XamTwitch.Services;

namespace XamTwitch.ViewModels
{
    public class PlayerPageViewModel : BaseNavigationViewModel
    {
        private readonly ITwitchAnonymousHttpService _twitchAnonymousHttpService;
        private readonly ITwitchPlaylistHttpService _twitchPlaylistHttpService;

        private TwitchStream _stream;
        public TwitchStream Stream
        {
            get => _stream;
            set
            {
                if(RaiseAndUpdate(ref _stream, value))
                {
                    FetchTwitchStreamAsync().HandleResult();
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
            _twitchAnonymousHttpService = ServiceContainer.Resolve<ITwitchAnonymousHttpService>();
            _twitchPlaylistHttpService = ServiceContainer.Resolve<ITwitchPlaylistHttpService>();
        }

        private async Task FetchTwitchStreamAsync()
        {
            if (Stream == null)
                return;

            var token = await _twitchAnonymousHttpService.GetTwitchTokenAsync(Stream.UserName);
            var streamUrl = await _twitchPlaylistHttpService.GetPlaylistUriAsync(Stream.UserName, token);
            StreamSource = streamUrl;
        }
    }
}
