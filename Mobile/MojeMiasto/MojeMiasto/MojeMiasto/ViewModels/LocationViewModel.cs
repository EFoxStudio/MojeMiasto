using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

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

        // Visibility City
        [ObservableProperty]
        bool cityCollectionVis = false;
        [ObservableProperty]
        bool newCityVis = false;

        // Visibility District
        [ObservableProperty]
        bool districtCollectionVis = false;
        [ObservableProperty]
        bool newDistrictsVis = false;


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
            NewCityVis = false;
            CityCollectionVis = true;

            List<City> citiesList = await citiesConn.GetList($"cities/search/{ CityEntry }");
            if(citiesList == null || citiesList.Count == 0)
            {
                NewCityVis = true;
                CityCollectionVis = false;
                return;
            }

            Cities = new ObservableCollection<City>(citiesList);
        }

        [RelayCommand]
        public async void SearchDistricts()
        {
            NewDistrictsVis = false;
            DistrictCollectionVis = true;

            int city_id = Preferences.Get("city_id", 0);

            if (city_id == 0)
                return;

            List<District> districtsList = await districtsConn.GetList($"districts/city_id/{ city_id }/search/{ DistrictEntry }");

            if (districtsList == null || districtsList.Count == 0)
            {
                NewDistrictsVis = true;
                DistrictCollectionVis = false;
                return;
            }

            Districts = new ObservableCollection<District>(districtsList);
        }

        [RelayCommand]
        public async void SetCity(City city)
        {
            int user_id = Preferences.Get("user_id", 0);

            if (user_id == 0)
                return;

            User user = await userConn.Get($"users/id/{ user_id }");

            user.city_id = city.id;

            await userConn.Put("users", user);

            Preferences.Set("city_id", city.id);

            CityEntry = city.name;
            

            NewCityVis = false;
            CityCollectionVis = false;
        }

        [RelayCommand]
        public async void SetDistrict(District district)
        {
            int user_id = Preferences.Get("user_id", 0);

            if (user_id == 0)
                return;

            User user = await userConn.Get($"users/id/{ user_id }");

            user.district_id = district.id;
            await userConn.Put("users", user);
            Preferences.Set("district_id", district.id);

            DistrictEntry = district.name;
            

            NewDistrictsVis = false;
            DistrictCollectionVis = false;
        }

        [RelayCommand]
        public void NewCity()
        {
            if (string.IsNullOrEmpty(CityEntry))
                return;

            City city = new City 
            {
                id = 0,
                name = CityEntry
            };

            citiesConn.Post("cities", city);
            SetCity(city);
        }

        [RelayCommand]
        public void NewDistrict()
        {
            int city_id = Preferences.Get("city_id", 0);

            if (city_id == 0)
                return;

            if (string.IsNullOrEmpty(DistrictEntry))
                return;

            District district = new District
            {
                id = 0,
                name = DistrictEntry,
                city_id = city_id
            };

            districtsConn.Post("districts", district);

            SetDistrict(district);

            
        }
    }
}
