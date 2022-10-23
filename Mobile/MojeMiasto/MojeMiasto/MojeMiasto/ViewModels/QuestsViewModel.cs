using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace MojeMiasto.ViewModels
{
    internal partial class QuestViewModel : BaseViewModel
    {

        public QuestViewModel()
        {
            IsBusy = true;
        }


        [ObservableProperty]
        ObservableCollection<UI_Quest> quests = new ObservableCollection<UI_Quest>();

        [RelayCommand]
        public async void GetQuests()
        {
            IsBusy = true;
            Quests.Clear();
            // Check if the user has the selected city
            int city_id = Preferences.Get("city_id", 0);
            if (city_id == 0)
            {
                IsBusy = false;
                return;
            }

            // Check if the user has the selected district
            int district_id = Preferences.Get("district_id", 0);
            if (district_id == 0)
            {
                IsBusy = false;
                return;
            }
            //Create Quest list and to the variable data
            List<Quest> data;

            if (selectedCity == true)
                // Get qusts from the city and assign to data
                data = await questConn.GetList($"quests/city_id/{city_id}");
            else
                // Get the qusts from the district and assign to data
                data = await questConn.GetList($"quests/district_id/{district_id}");

            // Check if the quest from the city and district has been downloaded at all
            if (data == null || data.Count == 0)
            {
                IsBusy = false;
                return;
            }

            foreach (Quest current in data)
            {
                Quests.Add(await QuestToUI(current));
            }

            IsBusy = false;

        }

        
    }

}
