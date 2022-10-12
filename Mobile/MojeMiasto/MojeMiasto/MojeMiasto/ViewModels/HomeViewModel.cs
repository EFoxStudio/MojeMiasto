using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class HomeViewModel : ObservableObject
    {
        [RelayCommand]
        public void OpenMenu()
        {
            Shell.Current.FlyoutIsPresented = true;

        }
        [RelayCommand]
        async void ToLoginPage()
        {

            await Shell.Current.GoToAsync(nameof(LoginPage));

        }
    }
}
