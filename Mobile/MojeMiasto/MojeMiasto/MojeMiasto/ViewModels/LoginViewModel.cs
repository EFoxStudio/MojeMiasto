using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class LoginViewModel : ObservableObject
    {
        [RelayCommand]
        public void OpenMenu()
        {
            Shell.Current.FlyoutIsPresented = true;

        }
    }
}
