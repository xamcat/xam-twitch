using System;
using System.Reflection;
using Microsoft.MobCAT;
using Microsoft.MobCAT.Forms.Services;
using Microsoft.MobCAT.MVVM.Abstractions;
using XamTwitch.Services;
using XamTwitch.Views;

namespace XamTwitch
{
    public static class Bootstrap
    {
        public static void Begin(Action platformSpecificBegin = null)
        {
            var navigationService = new NavigationService();
            navigationService.RegisterViewModels(typeof(BrowsePage).GetTypeInfo().Assembly);

            ServiceContainer.Register<INavigationService>(navigationService);
            ServiceContainer.Register<ITwitchHttpService>(new TwitchHttpService());
            ServiceContainer.Register<ITwitchPlaylistHttpService>(new TwitchPlaylistHttpService());
            ServiceContainer.Register<ITwitchAuthHttpService>(new TwitchAuthHttpService());
            platformSpecificBegin?.Invoke();
        }
    }
}
