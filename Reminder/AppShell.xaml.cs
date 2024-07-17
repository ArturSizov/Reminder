using Reminder.Pages;

namespace Reminder
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(UserProfilePage), typeof(UserProfilePage));
        }
    }
}
