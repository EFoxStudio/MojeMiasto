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
    internal partial class QuestViewModel : BaseViewModel
    {



        //Variable to get info from database
        Connection<City> cityCon = new Connection<City>("https://api.efox.com.pl/mycity/");
        Connection<District> districtCon = new Connection<District>("https://api.efox.com.pl/mycity/");
        Connection<Quest> questsConn = new Connection<Quest>("https://api.efox.com.pl/mycity/");
        Connection<User> userConn = new Connection<User>("https://api.efox.com.pl/mycity/");
        Connection<Hired> hiredConn = new Connection<Hired>("https://api.efox.com.pl/mycity/");
        public QuestViewModel()
        {
            cityCon.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            districtCon.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            questsConn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            userConn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            hiredConn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            IsBusy = true;
        }



        //List to store users
        [ObservableProperty]
        ObservableCollection<UI_Quest> quests = new ObservableCollection<UI_Quest>();
        private object stringConn;

        [RelayCommand]
        public async void GetQuests()
        {
            IsBusy = true;
            Quests.Clear();

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

            List<Quest> data;

            if (selectedCity == true)
                data = await questsConn.GetList($"quests/city_id/{city_id}");
            else
                data = await questsConn.GetList($"quests/district_id/{district_id}");

            if (data == null || data.Count == 0)
            {
                IsBusy = false;
                return;
            }



            foreach (Quest current in data)
            {
                City cityData = new City();
                District districtData = new District();
                User userData = new User();
                Hired hiredData = new Hired();


                if (current.city_id == 0)
                    cityData.name = "Niewybrane";
                else
                    cityData = await cityCon.Get($"cities/id/{current.city_id}");

                if (current.user_id == 0)
                    userData.name = "Nie istnieje";
                else
                    userData = await userConn.Get($"users/id/{current.user_id}");

                if (current.district_id == 0)
                    districtData.name = "Niewybrane";
                else
                    districtData = await districtCon.Get($"districts/id/{current.district_id}");


                if (current.hired_id == 0)
                    hiredData.name = "Niewybrane";
                else
                    hiredData = await hiredConn.Get($"districts/id/{current.district_id}");


                Quests.Add(new UI_Quest
                {
                    id = current.id,
                    name = current.name,
                    description = current.description,
                    user_id = current.user_id,
                    user = userData.name,
                    city_id = current.city_id,
                    city = cityData.name,
                    district_id = current.district_id,
                    district = districtData.name,
                    hired_id = current.hired_id,
                    hired = hiredData.name

                }) ;

            }

            IsBusy = false;

        }
        public void GoToQuest(UI_Quest data)
        {
           // Shell.Current.Navigation.PushAsync(new QuestPage(data));
        }

    }

}
