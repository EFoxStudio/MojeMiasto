using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using MojeMiasto.Views;
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
        // Create a variable cityEntry
        [ObservableProperty]
        string cityEntry;
        // Create a variable districtEntry
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

        //list to store cities
        [ObservableProperty]
        ObservableCollection<City> cities;

        //list to store districts
        [ObservableProperty]
        ObservableCollection<District> districts;

        [RelayCommand]
        public void GoToRoot()
        {
            Shell.Current.Navigation.PopToRootAsync();
        }

        [RelayCommand]
        public async void SearchCities()
        {
            NewCityVis = false;
            CityCollectionVis = true;

            List<City> citiesList = await cityConn.GetList($"cities/search/{ CityEntry }");
            if(citiesList == null || citiesList.Count == 0)
            {
                NewCityVis = true;
                CityCollectionVis = false;
                return;
            }

            Cities = new ObservableCollection<City>(citiesList);
        }
        //function to search districts
        [RelayCommand]
        public async void SearchDistricts()
        {
            NewDistrictsVis = false;
            DistrictCollectionVis = true;

            int city_id = Preferences.Get("city_id", 0);

            if (city_id == 0)
                return;

            List<District> districtsList = await districtConn.GetList($"districts/city_id/{ city_id }/search/{ DistrictEntry }");

            if (districtsList == null || districtsList.Count == 0)
            {
                NewDistrictsVis = true;
                DistrictCollectionVis = false;
                return;
            }

            Districts = new ObservableCollection<District>(districtsList);
        }
        //function to set cities
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
            Preferences.Set("district_id", 0);
            Shell.Current.BindingContext = new AppShellViewModel();

            CityEntry = FirstLetterToUpper(city.name);

            NewCityVis = false;
            CityCollectionVis = false;

            if (string.IsNullOrEmpty(DistrictEntry))
                return;
            DistrictEntry = null;
        }
        //function to set districts
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
            Shell.Current.BindingContext = new AppShellViewModel();

            DistrictEntry = FirstLetterToUpper(district.name);
            

            NewDistrictsVis = false;
            DistrictCollectionVis = false;
        }
        //function to add a new city
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

            cityConn.Post("cities", city);
            SetCity(city);
        }
        //function to add new districts
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

            districtConn.Post("districts", district);

            SetDistrict(district);
        }
    }
}
