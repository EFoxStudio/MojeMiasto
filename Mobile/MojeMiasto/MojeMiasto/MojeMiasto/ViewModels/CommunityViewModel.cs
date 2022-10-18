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
    internal partial class CommunityViewModel : BaseViewModel
    {


        //Variable to get info from database
        Connection<User> con = new Connection<User>("https://api.efox.com.pl/mycity/");
        Connection<City> cityCon = new Connection<City>("https://api.efox.com.pl/mycity/");
        Connection<District> districtCon = new Connection<District>("https://api.efox.com.pl/mycity/");
        Connection<string> stringConn = new Connection<string>("https://api.efox.com.pl/mycity/");
        Connection<Quest> questsConn = new Connection<Quest>("https://api.efox.com.pl/mycity/");

        public CommunityViewModel()
        {
            con.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            cityCon.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            districtCon.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            stringConn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            questsConn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            IsBusy = true;
        }

        

        //List to store users
        [ObservableProperty]
        ObservableCollection<UI_User> users = new ObservableCollection<UI_User>();


        [RelayCommand]
        public async void GetUsers()
        {
            IsBusy = true;
            Users.Clear();

            int city_id = Preferences.Get("city_id", 0);
            if (city_id == 0)
            {
                IsBusy = false;
                return;
            }

            int district_id = Preferences.Get("district_id", 0);
            if (district_id == 0)
            {
                IsBusy = false;
                return;
            }

            List<User> data;

            if (selectedCity == true)
                data = await con.GetList($"users/city_id/{city_id}");
            else
                data = await con.GetList($"users/district_id/{district_id}");

            if (data == null || data.Count == 0)
            {
                IsBusy = false;
                return;
            }

            foreach (User current in data)
            {

                City cityData = new City();
                District districtData = new District();

                if (current.city_id == 0)
                    cityData.name = "Niewybrane";
                else
                    cityData = await cityCon.Get($"cities/id/{current.city_id}");

                if(current.district_id == 0)
                    districtData.name = "Niewybrane";
                else
                    districtData = await districtCon.Get($"districts/id/{current.district_id}");

                List<Quest> questsData = await questsConn.GetList($"quests/user_id/{current.id}");

                string imageUrl = await stringConn.Get($"users/icon/{ current.id }");

                

                Users.Add(new UI_User
                {
                    id = current.id,
                    name = current.name,
                    email = current.email,
                    city_id = current.city_id,
                    district_id = current.district_id,
                    points = current.points,
                    city = cityData.name,
                    district = districtData.name,
                    iconUrl = $"https://api.efox.com.pl/mycity/icons/{imageUrl}",
                    location = $"{FirstLetterToUpper(cityData.name)}, {FirstLetterToUpper(districtData.name)}",
                    quests = questsData
                });

            }

            IsBusy = false;



            //Users = new ObservableCollection<User>(data);
        }

        [RelayCommand]
        public void GoToUser(UI_User data)
        {
            Shell.Current.Navigation.PushAsync(new UserPage(data));
        }


    }
}
