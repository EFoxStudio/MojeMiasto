﻿using MojeMiasto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MojeMiasto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserQuestsPage : ContentPage
    {
        public UserQuestsPage()
        {
            InitializeComponent();
            BindingContext = new QuestsViewModel();
        }
    }
}