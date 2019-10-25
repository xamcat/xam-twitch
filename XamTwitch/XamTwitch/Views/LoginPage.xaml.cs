using Microsoft.MobCAT.Forms.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamTwitch.ViewModels;

namespace XamTwitch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BaseContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}