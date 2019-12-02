using System;
using System.Collections.Generic;
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


    }
}
