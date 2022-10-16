using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EFox.ApiConnection.Toolkit;
using Microsoft.AspNetCore.Http;
using MojeMiasto.Models;
using MojeMiasto.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class SettingsViewModel : BaseViewModel
    {
        
        public SettingsViewModel()
        {
            //conn.AddHeader("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
        }


        [RelayCommand]
        async void GetImage()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Wybierz zdjęcie"
            });

            if(result == null)
                return;

            //var stream = await result.OpenReadAsync();

            int user_id = Preferences.Get("user_id", 0);
            if (user_id == 0)
                return;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", "g84@RRGA%!bP8vNzK7p&uLXz&");
            using (var multipartFormContent = new MultipartFormDataContent())
            {
                var fileStreamContent = new StreamContent(File.OpenRead(result.FullPath));
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                multipartFormContent.Add(fileStreamContent, name: "files", fileName: "house.png");
                var response = await client.PostAsync($"https://api.efox.com.pl/mycity/users/icon/{user_id}", multipartFormContent);
            }

            GetUserIcon();
        }

        


        [RelayCommand]
        void GoLocation()
        {
            Shell.Current.GoToAsync(nameof(LocationPage));
        }

        [RelayCommand]
        void Logout()
        {
            Preferences.Set("user_id", 0);
            Preferences.Set("city_id", 0);
            Preferences.Set("district_id", 0);
            Application.Current.MainPage = new NavigationPage(new WelcomePage());
        }
    }
}
