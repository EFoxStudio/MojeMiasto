using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    public partial class NewQuestViewModel : BaseViewModel
    {

        [ObservableProperty]
        DateTime startDate;

        [ObservableProperty]
        DateTime endDate;


        [ObservableProperty]
        TimeSpan startTime;

        [ObservableProperty]
        TimeSpan endTime;


        public NewQuestViewModel()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }


        [ObservableProperty]
        bool isChecked;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string error;




        [RelayCommand]
        public async void Submit()
        {

            int cityId;
            // checking
            if (string.IsNullOrEmpty(Name) == true || Name.Length < 2 || Name.Length > 20)
            {
                Error = "Name must be 2 to 20 words!";
                return;
            }

            if (string.IsNullOrEmpty(Description) == true || Description.Length < 2 || Description.Length > 100)
            {
                Error = "Description must be 2 to 100 words!";
                return;
            }

            if (isChecked)
            {
                cityId = Preferences.Get("city_id", 0);
                if (cityId == 0)
                {
                    Error = "Najpierw wybierz swoje miasto";
                    return;
                }
            }
            else
            {
                cityId = 0;
            }

            int districId = Preferences.Get("district_id", 0);
            if(districId==0)
            {
                Error = "Najpierw wybierz swoją dzielnice";
                return;
            }
            int userId = Preferences.Get("user_id", 0);
            if (userId == 0)
            {
                Error = "Błąd związany z pobieraniem user_id";
                return;
            }

            StartDate += startTime;
            EndDate += endTime;

            Quest quest = new Quest
            {
                id = 0,
                name = Name,
                description = Description,
                user_id = Preferences.Get("user_id", 0),
                city_id = cityId,
                district_id = Preferences.Get("district_id", 0),
                create_date = StartDate,
                end_date = EndDate,
                hired_id = 0,
                done = false
            };
            Error = $"start: {StartDate}, end: { EndDate}";
            return;

            await questConn.Post("quests", quest);

        }

    }
}
