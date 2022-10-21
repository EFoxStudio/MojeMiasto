using MojeMiasto.ViewModels;
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
    public partial class NewQuestPage : ContentPage
    {
        public NewQuestPage()
        {
            InitializeComponent();
            BindingContext = new NewQuestViewModel();
        }

        public void OnCheck(object sender, CheckedChangedEventArgs e)
        {
            bool check = false;
            check = e.Value;
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            string startDate = startDatePicker.Date.ToString();
            string endDate = endDatePicker.Date.ToString();
        }

    }
}