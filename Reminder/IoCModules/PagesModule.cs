using Reminder.Pages;

namespace Reminder.IoCModules
{
    public static class PagesModule
    {
        public static void UsePages(this IServiceCollection services)
        {
            services.AddTransient<AppShell>();
            services.AddTransient<MainPage>();
        }

    }
}
