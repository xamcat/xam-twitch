using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamTwitch
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            //TODO : sorry profiler-person this is bad for app launch
            var user = SecureStorage.GetAsync(Constants.AppName).Result;

            if(user == null)
            {
                Shell.Current.GoToAsync("//login");
            }
            else
            {
                Shell.Current.GoToAsync("//main");
            }


        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
