using Reminder.ViewModels;

namespace Reminder.IoCModules
{
    public static class ViewModelsModule
    {
        public static void UseViewModules(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
        }

    }
}
