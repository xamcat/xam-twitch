using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.MobCAT;
using Microsoft.MobCAT.MVVM;
using Newtonsoft.Json;
using Xamarin.Essentials;
using XamTwitch.Models;
using XamTwitch.Services;

namespace XamTwitch.ViewModels
{
    public class PlayerPageViewModel : BaseNavigationViewModel
    {
        private readonly ITwitchHttpService _twitchHttpService;

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
            _twitchHttpService = ServiceContainer.Resolve<ITwitchHttpService>();
        }

        private async Task FetchTwitchStreamAsync()
        {
            try
            {
                if (Stream == null)
                    return;

                StreamSource = await _twitchHttpService.GetTwitchStreamUrlAsync(Stream.UserName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
