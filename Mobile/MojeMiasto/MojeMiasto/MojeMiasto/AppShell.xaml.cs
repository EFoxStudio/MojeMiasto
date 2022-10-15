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
            
            //Page overlapping when switching to another
            Routing.RegisterRoute(nameof(QuestsPage), typeof(QuestsPage));
            Routing.RegisterRoute(nameof(CommunityPage), typeof(CommunityPage));
            Routing.RegisterRoute(nameof(LocationPage), typeof(LocationPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }
    }
}
