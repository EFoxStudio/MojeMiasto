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
        // Getting data from form
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

        // Event after clicked button
        [RelayCommand]
        public async void Register()
        {
            // Completing the correctness of data
            if (string.IsNullOrEmpty(Name))
                return;
            if (string.IsNullOrEmpty(Surname))
                return;
            if (string.IsNullOrEmpty(Email))
                return;
            if (string.IsNullOrEmpty(Password))
                return;
            if (string.IsNullOrEmpty(RepeatPassword))
                return;

            // Checking email
            bool validEmail = IsValidEmail(Email);
            if (validEmail != true)
            {
                Error = "Nieprawidłowy adres email!";
                return;
            }

            // Checking name
            if (Name.Length < 2 || Name.Length > 20)
            {
                Error = "Imię musi zawierać od 2 do 20 znaków!";
                return;
            }

            // Checking Surname
            if (Surname.Length < 2 || Surname.Length > 30)
            {
                Error = "Nazwisko musi zawierać od 2 do 30 znaków!";
                return;
            }

            // Checking password
            if (Password.Length < 8 || Surname.Length > 20)
            {
                Error = "Hasło musi zawierać od 8 do 20 znaków!";
                return;
            }

            // Checking repeated password
            if (RepeatPassword != Password)
            {
                Error = "Hasła nie są identyczne!";
                return;
            }

            // Connecting with database to validate the data
            User user = await userConn.Get($"users/email/{Email}");

            //Function for not receiving data
            if (user != default(User))
            {
                Error = "Podany adres email już istnieje!";
                return;
            }

            // Creating new user with parameters from form
            User data = new User
            {
                id = 0,
                name = Name + " " + Surname,
                email = Email,
                password = Password
            };

            Error = "";
            if (await userConn.Post("users", data))
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        // Function to validate the email address
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
