namespace Reminder.IoCModules
{
    public static class RootModule
    {
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
