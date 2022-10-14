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
    public partial class RegisterViewModel : BaseViewModel
    {
        //Create a variable to be referenced with the base
        Connection<User> conn = new Connection<User>("https://api.efox.com.pl/mycity/");
        Connection<string> str_conn = new Connection<string>("https://api.efox.com.pl/mycity/");

        public RegisterViewModel()
        {
            conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            str_conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }


        [ObservableProperty]
        string name;

        [ObservableProperty]
        string surname;

        [ObservableProperty]
        string email;
  
        [ObservableProperty]
        string password;

        [ObservableProperty]
        string repeatPassword;

        [ObservableProperty]
        string error;

        //Function for checking data from API
        [RelayCommand]
        public async void Register()
        {
            if (name.Length > 1 && surname.Length > 1 && email.Length > 1 && password.Length > 1 && repeatPassword.Length > 1) 
            {

                // Checking email
                bool validEmail = IsValidEmail(email);
                if (validEmail != true)
                {
                    Error = "Incorrect email address!";
                    return;
                }
                
                // Checking name
                if(name.Length<3 || name.Length > 20)
                {
                    Error = "Name must be 3 to 20 words!";
                    return;
                }

                // Checking Surname
                if (surname.Length < 1 || surname.Length > 30)
                {
                    Error = "Surname must be 1 to 30 words!";
                    return;
                }

                // Checking password
                if (Password.Length < 8 || surname.Length > 20)
                {
                    Error = "Password must be 8 to 20 words!";
                    return;
                }

                // Checking repeated password
                if (RepeatPassword != password)
                {
                    Error = "Password isn't the same!";
                    return;
                }

                // Connecting with database to validate the data

                User user = await conn.Get($"users/email/{Email}");

                //Function for not receiving data
                if (user != default(User))
                {
                    Error = "This email address is already exist!";
                    return;
                }

                User data = new User
                {
                    id = 0,
                    name = Name +" "+ Surname,
                    email = Email,
                    password = Password
                };

                // Correct register
                Error = "Valid verification";
                bool done = await conn.Post("users", data);
                if (done)
                {
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                }
                
                

            }
            else
            {
                Error = "Put correct data!";
            }
        }

        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }


    }
}
