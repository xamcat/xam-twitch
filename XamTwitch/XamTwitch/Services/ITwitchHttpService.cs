using System;
using System.Threading.Tasks;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public interface ITwitchHttpService
    {
        Task<TwitchGames> GetTwitchGamesAsync(string gameName);
        Task<TwitchStreams> GetTwitchStreamsAsync();
        Task<string> GetTwitchStreamUrlAsync(string userName);
    }
}
