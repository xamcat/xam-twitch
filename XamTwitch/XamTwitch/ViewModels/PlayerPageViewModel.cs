using System.Threading.Tasks;
using Microsoft.MobCAT.MVVM;
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
                RaiseAndUpdate(ref _stream, value);
                Raise(nameof(StreamSource));
            }
        }

        public string StreamSource
        {
            get
            {
                if (_stream == null)
                    return null;

                var source = $"https://player.twitch.tv/?video={Stream.Id}&channel={Stream.UserName}";
                return source;
            }
        }

        public PlayerPageViewModel()
        {
        }
    }
}
