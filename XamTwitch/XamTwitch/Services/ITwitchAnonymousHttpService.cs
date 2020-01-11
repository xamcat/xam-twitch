using System.Threading.Tasks;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public interface ITwitchAnonymousHttpService
    {
        Task<TwitchToken> GetTwitchTokenAsync(string userName);
    }
}
