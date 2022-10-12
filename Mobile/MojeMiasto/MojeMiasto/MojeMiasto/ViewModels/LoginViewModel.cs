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

        //Create a variable to be referenced with the base
        Connection<User> conn = new Connection<User>("https://api.efox.com.pl/mycity/");



        //Create a varible email
        [ObservableProperty]
        string email;
        //Create a varible password
        [ObservableProperty]
        string password;

        //Function for checking data from API
        [RelayCommand]
        public async void Login()
        {
            User user = await conn.Get($"users/email/{ Email }");

            //Function for not receiving data
            if (user == default(User))
                return;

            if (user.password == Password)
            {
                await Shell.Current.GoToAsync(nameof(HomePage));
            }

        }

     
    }
}
