using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class UserViewModel : BaseViewModel
    {
        // Create a variable user
        [ObservableProperty]
        UI_User user;
        public UserViewModel(UI_User data)
        {

            //Assign a user to a date
            user = data;
        }

        [RelayCommand]
        public async void ToUIQuest(Quest data)
        {
            UI_Quest quest = await QuestToUI(data);
            GoToQuest(quest);
        }
    }
}
