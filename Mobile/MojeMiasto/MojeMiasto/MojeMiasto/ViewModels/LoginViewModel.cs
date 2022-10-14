using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {

        //Create a variable to be referenced with the base
        Connection<User> conn = new Connection<User>("https://api.efox.com.pl/mycity/");
        Connection<string> str_conn = new Connection<string>("https://api.efox.com.pl/mycity/");

        public LoginViewModel()
        {
            conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            str_conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }

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

            string hashPass = await str_conn.PostWithReturn($"users/hash", Password);

            if (user.password == hashPass)
            {
                await Shell.Current.GoToAsync(nameof(HomePage));
                Preferences.Set("user_id", user.id);
            }
            
        }

     
    }
}
