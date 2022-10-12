using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [RelayCommand]
        public void OpenMenu()
        {
            Shell.Current.FlyoutIsPresented = true;

        }

        [RelayCommand]
        public async void ToLoginPage()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

    }
}
