using SDK.Base.Themes;
using SDK.Base.Abstractions;
using SDK.Base.Extensions;
using SDK.Base.Services;

namespace Reminder.IoCModules
{
    /// <summary>
    /// Services Ioc
    /// </summary>
    public static class ServicesModule
    {
        /// <summary>
        /// Use common cervices Ioc
        /// </summary>
        /// <param name="services"></param>
        public static void UseCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<IThemesManager, ThemesManager>();
            services.AddSingleton<IPhotoManager, PhotoManager>();
            services.AddSingleton<IDialogService, DialogService>();
        }
    }
}
