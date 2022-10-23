using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Models;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel
    {


        // Create a variable name
        [ObservableProperty]
        string name;
        // Create a variable surname
        [ObservableProperty]
        string surname;
        // Create a variable email
        [ObservableProperty]
        string email;
        // Create a variable password
        [ObservableProperty]
        string password;
        // Create a variable repeatPassword
        [ObservableProperty]
        string repeatPassword;
        // Create a variable error
        [ObservableProperty]
        string error;

        //Function for checking data from API
        [RelayCommand]
        public async void Register()
        {

            // Check if the field is not empty
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
                Error = "Incorrect email address!";
                return;
            }

            // Checking name
            if (Name.Length < 3 || Name.Length > 20)
            {
                Error = "Name must be 3 to 20 words!";
                return;
            }

            // Checking Surname
            if (Surname.Length < 1 || Surname.Length > 30)
            {
                Error = "Surname must be 1 to 30 words!";
                return;
            }

            // Checking password
            if (Password.Length < 8 || Surname.Length > 20)
            {
                Error = "Password must be 8 to 20 words!";
                return;
            }

            // Checking repeated password
            if (RepeatPassword != Password)
            {
                Error = "Password isn't the same!";
                return;
            }

            // Connecting with database to validate the data

            User user = await userConn.Get($"users/email/{Email}");

            //Function for not receiving data
            if (user != default(User))
            {
                Error = "This email address is already exist!";
                return;
            }

            User data = new User
            {
                id = 0,
                name = Name + " " + Surname,
                email = Email,
                password = Password
            };

            if (await userConn.Post("users", data))
            {
                await Application.Current.MainPage.Navigation.PopAsync();
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


        [RelayCommand]
        void Back()
        {
            Application.Current.MainPage.Navigation.PopAsync(false);
        }


    }
}
