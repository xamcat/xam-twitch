using System;
using Xamarin.UITest;

namespace XamTwitch.UITests.Pages
{
    public class FollowingPage : BasePage
    {
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = x => x.Marked("Following"),
            iOS = x => x.Marked("Following")
        };

        public FollowingPage() 
        {
            if (OnAndroid)
            {

            }

            if (OniOS)
            {

            }
        }
    }
}
