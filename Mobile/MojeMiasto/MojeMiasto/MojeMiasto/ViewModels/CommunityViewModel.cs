using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace MojeMiasto.ViewModels
{
    internal partial class CommunityViewModel : BaseViewModel
    {
        public CommunityViewModel()
        {
            IsBusy = true;
        }

        

        //List to store users
        [ObservableProperty]
        ObservableCollection<UI_User> users = new ObservableCollection<UI_User>();


        [RelayCommand]
        public async void GetUsers()
        {
            IsBusy = true;
            Users.Clear();

            int city_id = Preferences.Get("city_id", 0);
            if (city_id == 0)
            {
                IsBusy = false;
                return;
            }

            int district_id = Preferences.Get("district_id", 0);
            if (district_id == 0)
            {
                IsBusy = false;
                return;
            }

            List<User> data;

            if (selectedCity == true)
                data = await userConn.GetList($"users/city_id/{city_id}");
            else
                data = await userConn.GetList($"users/district_id/{district_id}");

            if (data == null || data.Count == 0)
            {
                IsBusy = false;
                return;
            }

            foreach (User current in data)
            {
                Users.Add(await UserToUI(current));
            }

            IsBusy = false;
        }


    }
}
