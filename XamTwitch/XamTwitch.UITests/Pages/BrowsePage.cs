using System;
using Xamarin.UITest;
// Aliases Func<AppQuery, AppQuery> with Query
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamTwitch.UITests.Pages
{
    public class BrowsePage : BasePage
    {
        protected override PlatformQuery Trait => new PlatformQuery
        {
            Android = null,
            iOS = null
        };

        public BrowsePage()
        {
            if(OnAndroid)
            {

            }

            if(OniOS)
            {

            }
        }
    }
}
