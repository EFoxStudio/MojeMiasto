using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
        ObservableCollection<User> usersList = new ObservableCollection<User>();

        [ObservableProperty]
        string cityNr;

        [RelayCommand]
        public async void GetUsers()
        {
            List<User> users = await con.GetList($"users/city_id/{CityNr}");

            foreach (User cur in users)
            {
                UsersList.Add(cur);
            }

        }

    }
}
