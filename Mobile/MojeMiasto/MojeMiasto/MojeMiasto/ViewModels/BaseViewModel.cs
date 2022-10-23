using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using MojeMiasto.Models;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    //Usable items for everything :)
    public partial class BaseViewModel : ObservableObject
    {
        //creating variables
        [ObservableProperty]
        public bool isBusy;

        public bool selectedCity;

        [ObservableProperty]
        string districtColor;

        [ObservableProperty]
        string cityColor;


        [ObservableProperty]
        string userIcon;
        //creating connections to api
        private string baseAdress = "https://api.efox.com.pl/mycity/";
        private Header header = new Header("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        public Connection<string> stringConn;
        public Connection<City> cityConn;
        public Connection<District> districtConn;
        public Connection<Quest> questConn;
        public Connection<User> userConn;

        //constructor to define headers for connections
        public BaseViewModel()
        {
            stringConn = new Connection<string>(baseAdress, header);
            cityConn = new Connection<City>(baseAdress, header);
            districtConn = new Connection<District>(baseAdress, header);
            questConn = new Connection<Quest>(baseAdress, header);
            userConn = new Connection<User>(baseAdress, header);


            GetUserIcon();

            DistrictColor = "#8537f0";
            CityColor = "#FFFCF2";
            selectedCity = false;
        }
        //function to get icons from users
        public async void GetUserIcon()
        {
            int user_id = Preferences.Get("user_id",0);
            if (user_id == 0)
                return;

            string fileName = await stringConn.Get($"users/icon/{ user_id }");

            UserIcon = $"https://api.efox.com.pl/mycity/icons/{fileName}";
        }

        public string FirstLetterToUpper(string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }


        public async Task<UI_User> UserToUI(User data)
        {
            City cityData = new City();
            District districtData = new District();

            if (data.city_id == 0)
                cityData.name = "Niewybrane";
            else
                cityData = await cityConn.Get($"cities/id/{data.city_id}");

            if (data.district_id == 0)
                districtData.name = "Niewybrane";
            else
                districtData = await districtConn.Get($"districts/id/{data.district_id}");

            List<Quest> questsData = await questConn.GetList($"quests/user_id/{data.id}");

            string imageUrl = await stringConn.Get($"users/icon/{ data.id }");



            return new UI_User
            {
                id = data.id,
                name = data.name,
                email = data.email,
                password = data.password,
                city_id = data.city_id,
                district_id = data.district_id,
                points = data.points,
                city = cityData.name,
                district = districtData.name,
                iconUrl = $"https://api.efox.com.pl/mycity/icons/{imageUrl}",
                location = $"{FirstLetterToUpper(cityData.name)}, {FirstLetterToUpper(districtData.name)}",
                quests = questsData
            };
        }

        public async Task<UI_Quest> QuestToUI(Quest data)
        {
            City cityData = new City();
            District districtData = new District();
            User userData = new User();
            User hiredData = new User();
            bool IsHired = true;

            if (data.hired_id == 0)
                IsHired = false;

            if (data.city_id == 0)
                cityData.name = "Niewybrane";
            else
                cityData = await cityConn.Get($"cities/id/{data.city_id}");

            if (data.user_id == 0)
                userData.name = "Nie istnieje";
            else
                userData = await userConn.Get($"users/id/{data.user_id}");

            if (data.district_id == 0)
                districtData.name = "Niewybrane";
            else
                districtData = await districtConn.Get($"districts/id/{data.district_id}");


            if (data.hired_id == 0)
                hiredData.name = "Nie ma";
            else
                hiredData = await userConn.Get($"users/id/{data.hired_id}");

            UI_User uiUserData = await UserToUI(userData);
            UI_User uiHiredData = await UserToUI(hiredData);


            return new UI_Quest
            {
                id = data.id,
                name = data.name,
                description = data.description,
                user_id = data.user_id,
                user = uiUserData,
                city_id = data.city_id,
                city = cityData.name,
                district_id = data.district_id,
                district = districtData.name,
                isHired = IsHired,
                hired_id = data.hired_id,
                hired = uiHiredData,
                done = data.done,
                create_date = data.create_date,
                end_date = data.end_date,
                location = $"{FirstLetterToUpper(cityData.name)}, {FirstLetterToUpper(districtData.name)}"
            };
        }


        public User UIToUser(UI_User data)
        {
            return new User 
            { 
                id = data.id,
                name=data.name,
                email = data.email,
                password = data.password,
                city_id=data.city_id,
                district_id=data.district_id,
                points = data.points
            };
        }

        public Quest UIToQuest(UI_Quest data)
        {
            return new Quest
            {
                id =data.id,
                name = data.name,
                description = data.description,
                user_id = data.user_id,
                city_id = data.city_id,
                district_id = data.district_id,
                create_date=data.create_date,
                end_date=data.end_date,
                hired_id=data.hired_id,
                done = data.done
            };
        }

        [RelayCommand]
        public void OpenMenu()
        {
            Shell.Current.FlyoutIsPresented = true;
        }

        [RelayCommand]
        public async void GoBack()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        [RelayCommand]
        public async void GoAccount()
        {
            await Shell.Current.Navigation.PushAsync(new SettingsPage());
        }

        [RelayCommand]
        public void GoToQuest(UI_Quest data)
        {
            Shell.Current.Navigation.PushAsync(new DetailQuestPage(data));
        }

        [RelayCommand]
        public void GoToUser(UI_User data)
        {
            Shell.Current.Navigation.PushAsync(new UserPage(data));
        }

        [RelayCommand]
        public async void GoToNewQuest()
        {
            await Shell.Current.Navigation.PushAsync(new NewQuestPage());
        }

        [RelayCommand]
        public void ChoseCity()
        {
            if (IsBusy)
                return;

            DistrictColor = "#FFFCF2";
            CityColor = "#8537f0";
            selectedCity = true;
            IsBusy = true;
        }

        [RelayCommand]
        public void ChoseDistrict()
        {
            if (IsBusy)
                return;

            DistrictColor = "#8537f0";
            CityColor = "#FFFCF2";
            selectedCity = false;
            IsBusy = true;
        }
    }
}
