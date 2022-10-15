﻿using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class WelcomeViewModel : BaseViewModel
    {

        [RelayCommand]
        public async void GoRegister()
        {
            Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(), false);
        }

        [RelayCommand]
        public async void GoLogin()
        {
            Application.Current.MainPage.Navigation.PushAsync(new LoginPage(), false);
        }
    }
}
