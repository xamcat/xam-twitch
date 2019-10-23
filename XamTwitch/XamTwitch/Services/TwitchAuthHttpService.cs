using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamTwitch.Models;

namespace XamTwitch.Services
{
    public class TwitchAuthHttpService
    {
        HttpClient httpClient = new HttpClient();

        public TwitchAuthHttpService(string access_token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
        }

        public async Task<TwitchUser> GetUserAsync()
        {
            var response = await httpClient.GetAsync("https://api.twitch.tv/helix/users");
            var test = await response.Content.ReadAsStringAsync();
            TwitchUser user = JsonConvert.DeserializeObject<TwitchUser>(test);

            return user;
        }
    }
}
