using MojeMiasto.ViewModels;
using MojeMiasto.Views;
using Xamarin.Forms;
using LoginPage = MojeMiasto.Views.LoginPage;

namespace MojeMiasto
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();


        }
    }
}
