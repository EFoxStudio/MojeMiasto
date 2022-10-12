using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class LoginViewModel : ObservableObject
    {
<<<<<<< Updated upstream
=======
        Connection<User> conn = new Connection<User>("https://api.efox.com.pl/mycity/");

        public LoginViewModel()
        {
            conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }


        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;


>>>>>>> Stashed changes
        [RelayCommand]
        public void OpenMenu()
        {
<<<<<<< Updated upstream
            Shell.Current.FlyoutIsPresented = true;
=======
            User user = await conn.Get($"email/{ Email }");

            if (user == default(User))
                return;

            if (user.password == Password)
            {
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
>>>>>>> Stashed changes

        }
    }
}
