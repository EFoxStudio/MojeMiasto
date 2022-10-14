using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {

        Connection<Quest> conn = new Connection<Quest>("https://api.efox.com.pl/mycity/");

        public HomeViewModel()
        {
            conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }

        [ObservableProperty]
        ObservableCollection<Quest> quests;

        [RelayCommand]
        public async void GetQuests()
        {
            if (Preferences.Get("district_id", 0) == 0)
                return;
            List<Quest> data = await conn.GetList($"Quests/district_id/{Preferences.Get("district_id",0)}");
            if (data == null)
                return;

            Quests = new ObservableCollection<Quest>(data);
          
        }
    }
}
