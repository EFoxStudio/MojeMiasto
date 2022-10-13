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
    //Usable items for everything
    public partial class BaseViewModel : ObservableObject
    {

        //Pull out the menuu
        [RelayCommand]
        public void OpenMenu()
        {
            Shell.Current.FlyoutIsPresented = true;

        }
        

    }
}
