using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    //Usable items for everything
    public partial class BaseViewModel : ObservableObject
    {

        //Pull out the menuu
        
        [RelayCommand]
        public void OpenMenu()
        {
            Shell.Current.FlyoutIsPresented = true;

        }
        //Redirection to LoginPage
        [RelayCommand]
        public async void ToLoginPage()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

    }
}
