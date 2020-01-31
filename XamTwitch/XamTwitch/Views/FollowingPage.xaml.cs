using Microsoft.MobCAT.Forms.Pages;
using Xamarin.Forms.Xaml;
using XamTwitch.ViewModels;

namespace XamTwitch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FollowingPage : BaseContentPage<FollowingPageViewModel>
    {
        public FollowingPage()
        {
            InitializeComponent();
        }
    }
}