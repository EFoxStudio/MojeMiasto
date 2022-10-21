using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    public partial class DetailQuestViewModel : BaseViewModel
    {

        [ObservableProperty]
        public int user_id;

        [ObservableProperty]
        UI_Quest quest;

        public DetailQuestViewModel(UI_Quest data)
        {
            quest = data;
            User_id = Preferences.Get("user_id", 0);
        }


        [RelayCommand]
        public async void Refresh()
        {
            IsBusy = true;
            Quest data = await questConn.Get($"quests/id/{ Quest.id }");
            UI_Quest ui_data = await QuestToUI(data);
            Quest = ui_data;
            IsBusy = false;
        }






        [RelayCommand]
        public async void Hire()
        {
            Quest quest = UIToQuest(Quest);
            quest.hired_id = user_id;
            await questConn.Put("quests", quest);
            Refresh();
        }


        [RelayCommand]
        public void GoToRoot()
        {
            Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
