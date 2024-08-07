using Reminder.Pages;
using Reminder.ViewModels;

namespace Reminder
{
    public partial class AppShell
    {
        public AppShell(AppShellViewModel vm)
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

            Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));

            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

            BindingContext = vm;
        }
    }
}
