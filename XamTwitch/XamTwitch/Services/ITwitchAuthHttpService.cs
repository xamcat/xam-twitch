using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public interface ITwitchAuthHttpService
    {
        Task<TwitchUser> GetUserAsync();
    }
}
