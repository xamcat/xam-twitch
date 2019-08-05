using System;
using Xamarin.UITest;

namespace XamTwitch.UITests.Pages
{
    public class FollowingPage : BasePage
    {
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = null,
            iOS = null
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
