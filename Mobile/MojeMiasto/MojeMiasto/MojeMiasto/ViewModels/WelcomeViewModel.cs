using CommunityToolkit.Mvvm.Input;
using MojeMiasto.Views;
using Xamarin.Forms;

namespace MojeMiasto.ViewModels
{
    internal partial class WelcomeViewModel : BaseViewModel
    {

        [RelayCommand]
        public async void GoRegister()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(), false);
        }

        [RelayCommand]
        public async void GoLogin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage(), false);
        }
    }
}
