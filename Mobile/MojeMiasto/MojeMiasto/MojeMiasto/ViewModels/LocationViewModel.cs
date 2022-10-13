using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MojeMiasto.ViewModels
{
    internal partial class LocationViewModel : BaseViewModel
    {
        Connection<City> conn = new Connection<City>("https://api.efox.com.pl/mycity/");

        [ObservableProperty]
        string city;
        [ObservableProperty]
        string district;


        [ObservableProperty]
        ObservableCollection<City> cities;


        public LocationViewModel()
        {
            conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }



        [RelayCommand]
        public async void SearchCities()
        {
            List<City> citiesList = await conn.GetList($"cities/search/{ City }");
            if(citiesList == null)
                return;
            Cities = new ObservableCollection<City>(citiesList);
        }
    }
}
