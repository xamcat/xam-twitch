using Xamarin.UITest;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamTwitch.UITests
{
    public class ItemsPage : BasePage
    {
        readonly Query addToolbarButton;

        public ItemsPage(IApp app, Platform platform) : base(app, platform, "Browse")
        {
            addToolbarButton = x => x.Marked("Add");
        }

        public void TapAddToolbarButton()
        {
            app.Tap(addToolbarButton);

            app.Screenshot("Toolbar Item Tapped");
        }
    }
}