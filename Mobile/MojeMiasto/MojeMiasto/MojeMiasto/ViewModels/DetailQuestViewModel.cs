using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    public partial class DetailQuestViewModel : BaseViewModel
    {
        // Create a variable user_id
        [ObservableProperty]
        public int user_id;
        // Create a variable quest
        [ObservableProperty]
        UI_Quest quest;
        // Create a variable isDoneVis
        [ObservableProperty]
        public bool isDoneVis;

        [ObservableProperty]
        public bool hireVis;

        public DetailQuestViewModel(UI_Quest data)
        {
            quest = data;
            Refresh();
            User_id = Preferences.Get("user_id", 0);

            if (Quest.user_id == User_id && Quest.done == false && Quest.hired_id != 0)
                IsDoneVis = true;
            else
                IsDoneVis = false;

            if (Quest.user_id != User_id && Quest.done == false && Quest.hired_id == 0)
                HireVis = true;
            else
                HireVis = false;
        }

        //Function to refresh quests
        [RelayCommand]
        public async void Refresh()
        {
            IsBusy = true;
            Quest data = await questConn.Get($"quests/id/{ Quest.id }");
            UI_Quest ui_data = await QuestToUI(data);
            Quest = ui_data;
            if (Quest.user_id == User_id && Quest.done == false && Quest.hired_id != 0)
                IsDoneVis = true;
            else
                IsDoneVis = false;

            if (Quest.user_id != User_id && Quest.done == false && Quest.hired_id == 0)
                HireVis = true;
            else
                HireVis = false;

            IsBusy = false;
        }

        [RelayCommand]
        public async void Done()
        {
            
            Quest quest = UIToQuest(Quest);
            quest.done = true;
            await questConn.Put("quests", quest);

            User user = await userConn.Get($"users/id/{ Quest.hired_id }");
            user.points++;
            await userConn.Put("users", user);
            Refresh();
        }



        [RelayCommand]
        public async void Hire()
        {
            Refresh();
            if (Quest.hired_id != 0)
                return;

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
