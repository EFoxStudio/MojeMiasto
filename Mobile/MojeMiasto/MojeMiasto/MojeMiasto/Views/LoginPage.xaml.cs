using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeMiasto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public LoginPage()
        {
            InitializeComponent();

        }


        //A redirection table to LoginPage.xaml.cs
        async void ToLoginPageCommand()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
