using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
