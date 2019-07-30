using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.MobCAT;
using Microsoft.MobCAT.MVVM;
using XamTwitch.Models;
using XamTwitch.Services;

namespace XamTwitch.ViewModels
{
    public class DiscoverPageViewModel : BaseNavigationViewModel
    {
        private readonly ITwitchHttpService _twitchHttpService;

        private ObservableCollection<TwitchGame> _games;
        public ObservableCollection<TwitchGame> Games
        {
            get => _games;
            set => RaiseAndUpdate(ref _games, value);
        }

        public DiscoverPageViewModel()
        {
            _twitchHttpService = ServiceContainer.Resolve<ITwitchHttpService>();
        }

        public override async Task InitAsync()
        {
            var gamesResult = await _twitchHttpService.GetTwitchGamesAsync("Overwatch").ConfigureAwait(false);
            Games = new ObservableCollection<TwitchGame>(gamesResult.Games);
        }
    }
}
