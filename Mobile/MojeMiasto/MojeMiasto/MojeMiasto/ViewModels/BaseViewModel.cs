using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    //Usable items for everything
    public partial class BaseViewModel : ObservableObject
    {
        int user_id;


        [ObservableProperty]
        string userIcon;

        Connection<string> conn = new Connection<string>("https://api.efox.com.pl/mycity/");

        public BaseViewModel()
        {
            conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            GetUserIcon();

            user_id = Preferences.Get("user_id", 0);
        }

        //Pull out the menuu
        [RelayCommand]
        public void OpenMenu()
        {
            Shell.Current.FlyoutIsPresented = true;
        }

        [RelayCommand]
        public void GoBack()
        {
            Shell.Current.Navigation.PopAsync();
        }

        public async void GetUserIcon()
        {
            int user_id = Preferences.Get("user_id",0);
            if (user_id == 0)
                return;

            string fileName = await conn.Get($"users/icon/{ user_id }");

            UserIcon = $"https://api.efox.com.pl/mycity/icons/{fileName}";
        }

        public string FirstLetterToUpper(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        



    }
}
