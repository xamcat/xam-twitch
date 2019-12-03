using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using XamTwitch.UITests.Pages;

namespace XamTwitch.UITests
{
    public class Tests : BaseTestFixture
    {
        public Tests(Platform platform)
            : base(platform)
        {
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            app.Screenshot("Welcome to Xam.Twitch!");

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void Repl()
        {
            app.Repl();
        }

        [Test]
        public void LoginTest()
        {
            new LoginPage()
                .EnterCredentials("testSwak", "qfnh4ePcx4Mwjuvtrv")
                .Login();

            new FollowingPage();    
        }
    }
}
