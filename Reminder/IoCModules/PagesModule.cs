using Reminder.Pages;

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
            services.AddTransient<MainPage>();
            services.AddTransient<UserProfilePage>();
        }

    }
}
