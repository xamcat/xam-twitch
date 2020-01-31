using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.MobCAT;
using Microsoft.MobCAT.MVVM;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamTwitch.Services;

namespace XamTwitch.ViewModels
{
    public class ProfilePageViewModel : BaseNavigationViewModel
    {
        private readonly ITwitchAuthHttpService _twitchAuthHttpService;

        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                RaiseAndUpdate(ref userName, value);
            }
        }

        private ImageSource profileImageSource;
        public ImageSource ProfileImageSource
        {
            get => profileImageSource;
            set
            {
                RaiseAndUpdate(ref profileImageSource, value);
            }
        }
        
        public AsyncCommand LogoutCommand { get; }
        public ProfilePageViewModel()
        {
            _twitchAuthHttpService = ServiceContainer.Resolve<ITwitchAuthHttpService>();
            LogoutCommand = new AsyncCommand(Logout);
            UserName = "Username Incoming";
            ProfileImageSource = ImageSource.FromUri(new Uri("https://static-cdn.jtvnw.net/user-default-pictures-uv/215b7342-def9-11e9-9a66-784f43822e80-profile_image-70x70.png"));

        }

        public Task Logout()
        {
            SecureStorage.Remove(Constants.AppName);
            _twitchAuthHttpService.ClearCurrentUser();
            return Shell.Current.GoToAsync("//login");
        }

        public async override Task InitAsync()
        {
            var userString = SecureStorage.GetAsync(Constants.AppName).Result;
            var user_object = JsonConvert.DeserializeObject<List<Account>>(userString);
            //var user = await new TwitchAuthHttpService(user_object[0].Properties["access_token"]).GetUserAsync();
            var user = await _twitchAuthHttpService.GetUserAsync();
            if (user != null)
            {
                UserName = user.Data[0].DisplayName;
                ProfileImageSource = ImageSource.FromUri(user.Data[0].ProfileImageUrl);
            }
        }
    }
}