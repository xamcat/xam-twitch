using System;
using Xamarin.UITest;

namespace XamTwitch.UITests.Pages
{
    public class DiscoverPage : BasePage
    {
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = null,
            iOS = null
        };

        public DiscoverPage()
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
