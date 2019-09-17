using System.Threading.Tasks;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public interface ITwitchPlaylistHttpService
    {
        Task<string> GetPlaylistUriAsync(string channelName, TwitchToken token);
    }
}
