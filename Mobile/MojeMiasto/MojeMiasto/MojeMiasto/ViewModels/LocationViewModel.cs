using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;

namespace MojeMiasto.ViewModels
{
    public partial class LocationViewModel : BaseViewModel
    {
        Connection<City> citiesConn = new Connection<City>("https://api.efox.com.pl/mycity/");
        Connection<District> districtsConn = new Connection<District>("https://api.efox.com.pl/mycity/");
        Connection<User> userConn = new Connection<User>("https://api.efox.com.pl/mycity/");

        [ObservableProperty]
        string cityEntry;
        [ObservableProperty]
        string districtEntry;


        [ObservableProperty]
        ObservableCollection<City> cities;

        [ObservableProperty]
        ObservableCollection<District> districts;

        public LocationViewModel()
        {
            citiesConn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            userConn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            districtsConn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }



        [RelayCommand]
        public async void SearchCities()
        {
            List<City> citiesList = await citiesConn.GetList($"cities/search/{ CityEntry }");
            if(citiesList == null)
                return;
            Cities = new ObservableCollection<City>(citiesList);
        }

        [RelayCommand]
        public async void SearchDistricts()
        {
            List<District> districtsList = await districtsConn.GetList($"districts/search/{ DistrictEntry }");

            if (districtsList == null)
                return;

            Districts = new ObservableCollection<District>(districtsList);
        }


        [RelayCommand]
        public async void SetCity(City city)
        {
            if (Preferences.Get("User", 0) == 0)
                return;

            User user = await userConn.Get($"users/{ Preferences.Get("User", 0) }");

            user.city_id = city.id;
            await userConn.Put("users", user);

            CityEntry = city.name;
            Cities.Clear();
        }

        [RelayCommand]
        public async void SetDistrict(District district)
        {
            if (Preferences.Get("User", 0) == 0)
                return;

            User user = await userConn.Get($"users/{ Preferences.Get("User", 0) }");

            user.district_id = district.id;
            await userConn.Put("users", user);

            DistrictEntry = district.name;
            Districts.Clear();
        }
    }
}
