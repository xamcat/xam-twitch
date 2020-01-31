using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.MobCAT;
using Microsoft.MobCAT.MVVM;
using XamTwitch.Services;

namespace XamTwitch.ViewModels
{
    public class FollowingPageViewModel : BaseNavigationViewModel
    {
        readonly ITwitchAuthHttpService _twitchAuthHttpService;
        readonly ITwitchHttpService _twitchHttpService;
        ObservableCollection<ChannelFollowViewModel> _follows;

        public ObservableCollection<ChannelFollowViewModel> Follows
        {
            get => _follows;
            set => RaiseAndUpdate(ref _follows, value);
        }

        public FollowingPageViewModel()
        {
            _twitchAuthHttpService = ServiceContainer.Resolve<ITwitchAuthHttpService>();
            _twitchHttpService = ServiceContainer.Resolve<ITwitchHttpService>();
        }

        public override async Task InitAsync()
        {
            // Get ID for current user
            var currentUserId = await _twitchAuthHttpService.GetCurrentUserIdAsync().ConfigureAwait(false);

            // Get follows
            var channelFollows = await _twitchHttpService.GetChannelFollowsAsync(currentUserId).ConfigureAwait(false);

            // For each follow:
            var followIdValues = channelFollows.Follows.Select(i => i.ToId).ToList();

            // Get profile data
            var profileData = followIdValues.Select(i => _twitchHttpService.GetProfileAsync(i));

            // Get video data
            var videoData = followIdValues.Select(i => _twitchHttpService.GetUserVideosAsync(i));

            // TODO: Review throttling of parallel operations, handling exceptions/cancellation, cache, etc.
            await Task.WhenAll(Task.WhenAll(profileData), Task.WhenAll(videoData)).ConfigureAwait(false);

            var profiles = profileData.ToDictionary(
                i => i.Result.Data.FirstOrDefault().Id.ToString(),
                i => new KeyValuePair<string, string>(
                    i.Result.Data.FirstOrDefault().DisplayName,
                    i.Result.Data.FirstOrDefault().ProfileImageUrl.ToString()));

            // TODO: Review whether the API can do some of this filtering and sorting instead
            var minimumPublishedAtDateTime = DateTime.Now.Subtract(TimeSpan.FromDays(14));

            var videoCounts = videoData.ToDictionary(
                i => i.Result.Videos.FirstOrDefault().UserId,
                i => i.Result.Videos.Count(i2 => i2.PublishedAt > minimumPublishedAtDateTime));

            // Combine into a single type
            var follows = followIdValues.Select(i => new ChannelFollowViewModel
            {
                Username = profiles[i].Key,
                ProfileImageUrl = profiles[i].Value,
                NewVideos = videoCounts[i]
            })
            .OrderByDescending(i => i.NewVideos)
            .ThenBy(i => i.Username);

            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() => { Follows = new ObservableCollection<ChannelFollowViewModel>(follows); });
        }
    }
}