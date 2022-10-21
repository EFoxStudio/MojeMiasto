using MojeMiasto.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeMiasto
{
    public partial class App : Application
    {
        public static string startDatePicker { get; internal set; }

        public App()
        {
            InitializeComponent();

            if (Preferences.Get("user_id", 0) == 0)
                MainPage = new NavigationPage(new WelcomePage());
            else
                MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
