using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    public partial class NewQuestViewModel : BaseViewModel
    {

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string error;

        [RelayCommand]
        public async void Submit()
        {
            if (string.IsNullOrEmpty(Name))
                return;
            if (string.IsNullOrEmpty(Description))
                return;

            if (Name.Length < 2 || Name.Length > 20)
            {
                Error = "Name must be 2 to 20 words!";
                return;
            }

            if (Description.Length < 2 || Description.Length > 100)
            {
                Error = "Name must be 2 to 100 words!";
                return;
            }

        }

    }
}
