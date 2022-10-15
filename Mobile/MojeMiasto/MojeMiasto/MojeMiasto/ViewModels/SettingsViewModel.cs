using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class SettingsViewModel : BaseViewModel
    {

        [RelayCommand]
        void GoLocation()
        {
            Preferences.Set("user_id", 0);
            Shell.Current.GoToAsync(nameof(LocationPage));
        }

        [RelayCommand]
        void Logout()
        {
            Preferences.Set("user_id", 0);
            Application.Current.MainPage = new NavigationPage(new WelcomePage());
        }
    }
}
