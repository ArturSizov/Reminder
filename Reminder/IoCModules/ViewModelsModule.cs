using Reminder.ViewModels;

namespace Reminder.IoCModules
{
    /// <summary>
    /// Ioc view models
    /// </summary>
    public static class ViewModelsModule
    {
        /// <summary>
        /// Service view models
        /// </summary>
        /// <param name="services"></param>
        public static void UseViewModules(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
        }

    }
}
