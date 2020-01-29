using Microsoft.MobCAT.MVVM;

namespace XamTwitch.ViewModels
{
    public class ChannelFollowViewModel : BaseViewModel
    {
        string _username;
        string _profileImageUrl;
        int _newVideos;

        public string Username
        {
            get => _username;
            set => RaiseAndUpdate(ref _username, value);
        }

        public string ProfileImageUrl
        {
            get => _profileImageUrl;
            set => RaiseAndUpdate(ref _profileImageUrl, value);
        }

        public int NewVideos
        {
            get => _newVideos;
            set => RaiseAndUpdate(ref _newVideos, value);
        }
    }
}