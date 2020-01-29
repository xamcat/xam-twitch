using System.Threading.Tasks;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public interface ITwitchAuthHttpService
    {
        Task<TwitchUser> GetUserAsync();
        ValueTask<string> GetCurrentUserIdAsync();
        void ClearCurrentUser();
    }
}