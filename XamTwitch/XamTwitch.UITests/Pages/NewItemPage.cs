using Xamarin.UITest;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace XamTwitch.UITests
{
    public class NewItemPage : BasePage
    {
        readonly Query itemNameEntry, itemDescriptionEditor, saveToolbarItem;

        public NewItemPage(IApp app, Platform platform) : base(app, platform, "New Item")
        {
            if (OniOS)
            {
                itemNameEntry = x => x.Class("UITextField").Index(0);
                itemDescriptionEditor = x => x.Class("UITextView").Index(0);
            }
            else
            {
                itemNameEntry = x => x.Class("FormsEditText").Index(0);
                itemDescriptionEditor = x => x.Class("FormsEditText").Index(1);
            }

            saveToolbarItem = x => x.Marked("Save");
        }

        public void EnterItemName(string text)
        {
            EnterText(itemNameEntry, text);

            app.Screenshot("Entered Item Name");
        }

        public void EnterItemDescription(string text)
        {
            EnterText(itemDescriptionEditor, text);

            app.Screenshot("Entered Item Description");
        }

        public void TapSaveToolbarButton()
        {
            app.Tap(saveToolbarItem);

            app.Screenshot("Save Toolbar Item Tapped");
        }

        void EnterText(Query textBoxQuery, string text)
        {
            app.ClearText(textBoxQuery);
            app.EnterText(textBoxQuery, text);
            app.DismissKeyboard();
        }
    }
}