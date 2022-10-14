using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;

namespace MojeMiasto.ViewModels
{
    internal partial class CommunityViewModel : BaseViewModel
    {
        //Variable to get info from database
        Connection<User> con = new Connection<User>("https://api.efox.com.pl/mycity/");

        public CommunityViewModel()
        {
            con.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }


        //List to store users
        [ObservableProperty]
        ObservableCollection<User> users = new ObservableCollection<User>();


        [RelayCommand]
        public async void GetUsers()
        {
            int city_id = Preferences.Get("city_id", 0);
            if (city_id == 0)
                return;

            List<User> data = await con.GetList($"users/city_id/{city_id}");
            if(data == null)
                return;
            Users = new ObservableCollection<User>(data);

        }

    }
}
