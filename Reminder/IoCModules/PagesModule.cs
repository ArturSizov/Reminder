using CommunityToolkit.Maui;
using Reminder.Controls;
using Reminder.Pages;
using Reminder.ViewModels.Popup;

namespace Reminder.IoCModules
{
    /// <summary>
    /// Ioc pages
    /// </summary>
    public static class PagesModule
    {
        /// <summary>
        /// Service pages
        /// </summary>
        /// <param name="services"></param>
        public static void UsePages(this IServiceCollection services)
        {
            services.AddTransient<AppShell>();
            services.AddTransient<BasePage>();
            services.AddTransient<MainPage>();
            services.AddTransient<UserProfilePage>();
            services.AddTransient<SettingsPage>();

            //Register popup 
            services.AddTransientPopup<UserDialogPopup, UserDialogPopupViewModel>();
        }
    }
}
