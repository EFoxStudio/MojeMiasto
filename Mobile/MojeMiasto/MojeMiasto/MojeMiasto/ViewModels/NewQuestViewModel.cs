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
        // Getting data from form
        [ObservableProperty]
        DateTime startDate;

        [ObservableProperty]
        DateTime endDate;

        [ObservableProperty]
        TimeSpan startTime;

        [ObservableProperty]
        TimeSpan endTime;

        [ObservableProperty]
        bool isChecked;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string error;

        // Today's date
        public NewQuestViewModel()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }

        // Event after clicked button
        [RelayCommand]
        public async void Submit()
        {
            // Completing the correctness of data
            
            // Checking name
            if (string.IsNullOrEmpty(Name) == true || Name.Length < 2 || Name.Length > 20)
            {
                Error = "Imię musi zawierać od 2 do 20 znaków!";
                return;
            }

            // Checking description
            if (string.IsNullOrEmpty(Description) == true || Description.Length < 2 || Description.Length > 100)
            {
                Error = "Opis musi zawierać od 2 do 100 znaków!";
                return;
            }

            // Checking checkbox
            int cityId;
            if (isChecked)
            {
                cityId = Preferences.Get("city_id", 0);
                if (cityId == 0)
                {
                    Error = "Najpierw wybierz swoje miasto!";
                    return;
                }
            }
            else
            {
                cityId = 0;
            }

            // Checking district from API
            int districId = Preferences.Get("district_id", 0);
            if(districId==0)
            {
                Error = "Najpierw wybierz swoją dzielnice!";
                return;
            }
            
            // Checking user_id from API
            int userId = Preferences.Get("user_id", 0);
            if (userId == 0)
            {
                Error = "Błąd związany z pobieraniem danych!";
                return;
            }

            // Checking date
            if (StartDate.Day == EndDate.Day && StartDate.Month == StartDate.Month)
            {
                Error = "Nie można ustawić takiego terminu!";
                return;
            }

            // Chec
            if(startTime.Hours == 0 || endTime.Hours == 0)
            {
                Error = "Nie można ustawić takiego czasu terminu!";
                return;
            }

            // Connecting date and time to one variable
            StartDate += startTime;
            EndDate += endTime;

            // Creating new quest with parameters from form
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

            await questConn.Post("quests", quest);
            Error = "";
        }
    }
}
