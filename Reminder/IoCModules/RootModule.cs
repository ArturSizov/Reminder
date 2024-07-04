namespace Reminder.IoCModules
{
    public static class RootModule
    {
        /// <summary>
        /// Basic Ioc
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static MauiAppBuilder UseServices(this MauiAppBuilder builder)
        {
            RegisterServices(builder.Services);
            return builder;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.UseCommonServices();
            services.UseViewModules();
            services.UsePages();
        }

    }
}
