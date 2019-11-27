using System;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using WebQuery = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppWebQuery>;

namespace XamTwitch.UITests.Pages
{
    public class LoginPage : BasePage
    {
        readonly Query loginButton;
        readonly WebQuery usernameField;
        readonly WebQuery passwordField;
        readonly WebQuery webLoginButton;

        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("Welcome to Xam.Twitch!"),
            iOS = x => x.Marked("Welcome to Xam.Twitch!")
        };

        public LoginPage()
        {
            loginButton = x => x.Marked("Login Button");
            usernameField = x => x.Css("#username");
            passwordField = x => x.Css("#password>input");
            webLoginButton = x => x.Css(".primary.button.js-login-button");

            if (OnAndroid)
            {
            }

            if (OniOS)
            {
            }
        }

        public LoginPage EnterCredentials(string username, string password)
        {
            app.WaitForElement(loginButton);
            app.Tap(loginButton);

            app.Screenshot("Web Auth View Open");
            app.WaitForElement(usernameField);
            app.Tap(usernameField);
            app.EnterText(username);
            app.DismissKeyboard();
            app.Screenshot("Username Entered");

            app.EnterText(passwordField, password);
            app.Screenshot("Password Entered");


            return this;
        }

        public void Login()
        {
            app.WaitForElement(webLoginButton);
            app.Tap(webLoginButton);
        }
    }
}
