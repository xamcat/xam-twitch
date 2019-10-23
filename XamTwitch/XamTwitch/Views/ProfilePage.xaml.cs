using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.MobCAT.Forms.Pages;
using Xamarin.Auth;
using Xamarin.Forms;
using XamTwitch.Helpers;
using XamTwitch.Services;
using XamTwitch.ViewModels;

namespace XamTwitch.Views
{
    public partial class ProfilePage : BaseContentPage<ProfilePageViewModel>
    {
        //TODO: Update to use Xamarin.Essentials
        Account account;
        AccountStore store;

        public ProfilePage()
        {
            InitializeComponent();
            store = AccountStore.Create();
            ProfileImage.Source = ImageSource.FromUri(new Uri("https://static-cdn.jtvnw.net/user-default-pictures-uv/215b7342-def9-11e9-9a66-784f43822e80-profile_image-70x70.png"));
        }

        void LoginButton_Clicked(System.Object sender, System.EventArgs e)
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = Constants.iOSClientId;
                    redirectUri = Constants.iOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = Constants.AndroidClientId;
                    redirectUri = Constants.AndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(clientId,Constants.Scope, new Uri(Constants.AuthorizeUrl),new Uri(redirectUri),null,false);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            if (e.IsAuthenticated)
            {
                var request = e.Account;
                var access_token = request.Properties["access_token"];
                var user = await new TwitchAuthHttpService(access_token).GetUserAsync();

                if(user != null)
                {
                    UserName.Text = user.Data[0].DisplayName;
                    ProfileImage.Source = ImageSource.FromUri(user.Data[0].ProfileImageUrl);
                }
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }
    }
}
