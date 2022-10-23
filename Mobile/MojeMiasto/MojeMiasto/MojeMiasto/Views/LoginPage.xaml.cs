using CommunityToolkit.Mvvm.Input;
using MojeMiasto.ViewModels;
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
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}
