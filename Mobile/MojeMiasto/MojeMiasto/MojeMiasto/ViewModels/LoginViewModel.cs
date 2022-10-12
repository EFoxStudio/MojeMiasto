using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class LoginViewModel : BaseViewModel
    {
        Connection<User> conn = new Connection<User>("https://api.efox.com.pl/mycity/");


        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;


        [RelayCommand]
        public async void Login()
        {
            User user = await conn.Get($"users/email/{ Email }");

            if (user == default(User))
                return;

            if (user.password == Password)
            {
                //ToHomePage();
            }

        }

    }
}
