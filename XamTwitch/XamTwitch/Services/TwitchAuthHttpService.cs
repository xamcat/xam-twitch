using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.MobCAT.Services;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Essentials;
using XamTwitch.Helpers;
using XamTwitch.Models;

namespace XamTwitch.Services
{

    public class TwitchAuthHttpService : BaseHttpService, ITwitchAuthHttpService
    {
        string _currentUserId;
        public Account CurrentUser { get; set; }

        public TwitchAuthHttpService() : base(Constants.TwitchApiUri)
        {
            //placeholder logic check
            string userString = SecureStorage.GetAsync(Constants.AppName).Result;
            if (userString != null)
            {
                List<Account> user_object = JsonConvert.DeserializeObject<List<Account>>(userString);
                CurrentUser = user_object[0];
            }
            else
            {
                CurrentUser = new Account();
                CurrentUser.Properties["access_token"] = "";
            }

            Serializer = new NewtonsoftJsonSerializer();
        }

        void SetBearerTokenForRequest(HttpRequestMessage request)
            => request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Properties["access_token"]);


        public Task<TwitchUser> GetUserAsync()
        {
            return GetAsync<TwitchUser>($"helix/users",modifyRequest:(request) => SetBearerTokenForRequest(request));
        }

        public async ValueTask<string> GetCurrentUserIdAsync()
        {
            if (!string.IsNullOrWhiteSpace(_currentUserId))
                return _currentUserId;

            _currentUserId = await SecureStorage.GetAsync(Constants.CurrentUserId).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(_currentUserId))
            {
                var user = await GetUserAsync().ConfigureAwait(false);
                _currentUserId = user.Data.FirstOrDefault().Id.ToString();
                await SecureStorage.SetAsync(Constants.CurrentUserId, _currentUserId).ConfigureAwait(false);
            }                

            return _currentUserId;
        }

        public void ClearCurrentUser()
        {
            SecureStorage.Remove(Constants.CurrentUserId);
            _currentUserId = null;
        }
    }
}
