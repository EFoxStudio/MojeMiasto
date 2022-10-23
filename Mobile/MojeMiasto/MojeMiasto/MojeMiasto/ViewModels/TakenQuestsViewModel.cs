using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Models;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
namespace MojeMiasto.ViewModels
{
    public partial class TakenQuestsViewModel : BaseViewModel
    {
        public TakenQuestsViewModel()
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

            List<Quest> data;

            if (selectedCity == true)
                data = await questConn.GetList($"quests/city_id/{city_id}/hired_id/{user_id}");
            else
                data = await questConn.GetList($"quests/district_id/{district_id}/hired_id/{user_id}");

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
