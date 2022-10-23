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
        // Getting data from form
        [ObservableProperty]
        string email;
        
        [ObservableProperty]
        string password;

        [ObservableProperty]
        string error;

        //Function for checking data from API
        [RelayCommand]
        public async void Login()
        {
            User user = await userConn.Get($"users/email/{ Email }");

            //Function for not receiving data
            if (user == default(User))
            {
                Error = "Podano nieprawidłowe dane!";
                return;
            }

            // Function to checking validate data
            if(email == null || password == null)
            {
                Error = "Podano nieprawidłowe dane!";
                return;
            }

            string hashPass = await stringConn.PostWithReturn($"users/hash", Password);

            // If data is valid login
            if (user.password == hashPass)
            {
                Preferences.Set("user_id", user.id);
                Preferences.Set("city_id", user.city_id);
                Preferences.Set("district_id", user.district_id);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                Error = "Niepoprawne hasło!";
                return;
            }
            
        }


        [RelayCommand]
        void Back()
        {
            Application.Current.MainPage.Navigation.PopAsync(false);
        }

     
    }
}
