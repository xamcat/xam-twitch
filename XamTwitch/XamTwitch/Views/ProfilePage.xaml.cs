using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.MobCAT.Forms.Pages;
using Xamarin.Auth;
using Xamarin.Forms;
using XamTwitch.Helpers;
using XamTwitch.Services;
using XamTwitch.ViewModels;

namespace XamTwitch.Views
{
    public partial class ProfilePage : BaseContentPage<ProfilePageViewModel>
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
    }
}
