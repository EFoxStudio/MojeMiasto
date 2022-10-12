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

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        }
    }
}
