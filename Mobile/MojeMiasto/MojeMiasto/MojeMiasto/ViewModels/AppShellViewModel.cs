using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class AppShellViewModel : BaseViewModel
    {
        //Creating variables
        [ObservableProperty]
        string userCity;

        [ObservableProperty]
        string userName;

        public AppShellViewModel()
        {
            OnAppearing();
        }
        //Function which 'creates' the default view of the app
        public async void OnAppearing()
        {
            int user_id = Preferences.Get("user_id", 0);
            if (user_id == 0)
                return;

            User user = await userConn.Get($"users/id/{ user_id }");

            if (user == null)
                return;

            UserName = user.name;

            if (user.city_id == 0)
                UserCity = "niewybrane";



            City city = await cityConn.Get($"cities/id/{user.city_id}");
            if (city == null)
                return;


            UserCity = FirstLetterToUpper(city.name);
        }

        
        [RelayCommand]
        public void GoSettings()
        {
            Shell.Current.FlyoutIsPresented = false;
            GoAccount();
        }

    }
}
