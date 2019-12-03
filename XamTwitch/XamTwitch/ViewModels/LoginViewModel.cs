using Microsoft.MobCAT.MVVM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Auth;
using Xamarin.Forms;
using XamTwitch.Helpers;
using XamTwitch.Services;

namespace XamTwitch.ViewModels
{
    public class LoginViewModel : BaseNavigationViewModel
    {
        public Microsoft.MobCAT.MVVM.Command LoginCommand { get; }
        public string UserName { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new Microsoft.MobCAT.MVVM.Command(Login);
        }

        public void Login()
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

            var authenticator = new OAuth2Authenticator(clientId, Constants.Scope, new Uri(Constants.AuthorizeUrl), new Uri(redirectUri), null, false);

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
                //var access_token = request.Properties["access_token"];
                await SecureStorageAccountStore.SaveAsync(e.Account, Constants.AppName);
                await Shell.Current.GoToAsync("//main");
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
