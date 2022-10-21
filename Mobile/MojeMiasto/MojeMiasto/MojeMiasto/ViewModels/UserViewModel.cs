using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class UserViewModel : BaseViewModel
    {
        [ObservableProperty]
        UI_User user;
        public UserViewModel(UI_User data)
        {
            user = data;
        }

        [RelayCommand]
        public void GoToRoot()
        {
            Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
