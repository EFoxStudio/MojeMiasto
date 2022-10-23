using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    public partial class UserQuestsViewModel : BaseViewModel
    {
       

        public UserQuestsViewModel()
        {
            IsBusy = true;
        }



        //List to store users
        [ObservableProperty]
        ObservableCollection<UI_Quest> userQuests = new ObservableCollection<UI_Quest>();

        [RelayCommand]
        public async void GetQuests()
        {
            IsBusy = true;
            UserQuests.Clear();

            int user_id = Preferences.Get("user_id", 0);
            if (user_id == 0)
            {
                IsBusy = false;
                return;
            }

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
            //Will create Quest list under variable date
            List<Quest> data;

            if (selectedCity == true)
                data = await questConn.GetList($"quests/city_id/{city_id}/user_id/{user_id}");
            else
                data = await questConn.GetList($"quests/district_id/{district_id}/user_id/{user_id}");

            if (data == null || data.Count == 0)
            {
                IsBusy = false;
                return;
            }

            foreach (Quest current in data)
            {
                UserQuests.Add(await QuestToUI(current));
            }

            IsBusy = false;

        }
    }
}
