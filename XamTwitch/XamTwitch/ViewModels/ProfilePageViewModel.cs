using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.MobCAT.MVVM;
using Newtonsoft.Json;
using Xamarin.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamTwitch.Helpers;
using XamTwitch.Services;

namespace XamTwitch.ViewModels
{
    public class ProfilePageViewModel : BaseNavigationViewModel
    {
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
        
        public Microsoft.MobCAT.MVVM.Command LogoutCommand { get; }
        public ProfilePageViewModel()
        {
            LogoutCommand = new Microsoft.MobCAT.MVVM.Command(Logout);
            UserName = "Username Incoming";
            ProfileImageSource = ImageSource.FromUri(new Uri("https://static-cdn.jtvnw.net/user-default-pictures-uv/215b7342-def9-11e9-9a66-784f43822e80-profile_image-70x70.png"));

        }
        
        public async void Logout()
        {
            SecureStorage.Remove(Constants.AppName);
            await Shell.Current.GoToAsync("//login");
        }

        public async override Task InitAsync()
        {
            var userString = SecureStorage.GetAsync(Constants.AppName).Result;
            var user_object = JsonConvert.DeserializeObject<List<Account>>(userString);
            var user = await new TwitchAuthHttpService(user_object[0].Properties["access_token"]).GetUserAsync();

            if (user != null)
            {
                UserName = user.Data[0].DisplayName;
                ProfileImageSource = ImageSource.FromUri(user.Data[0].ProfileImageUrl);
            }
        }
    }
}
