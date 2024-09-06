using SDK.Base.Themes;
using SDK.Base.Abstractions;
using SDK.Base.Extensions;
using SDK.Base.Services;
using CommunityToolkit.Maui;
using Reminder.Models;
using Reminder.Abstractions;
using Reminder.Managers;
using Reminder.DataAccessLayer.DAO;
using Reminder.DataAccessLayer;
using Reminder.Auxiliary;

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
            services.AddSingleton<IDataProvider<UserDAO>, UserLiteDbProvider>();
            services.AddSingleton<IDataManager<User>, UserManager>();
            services.AddSingleton<IPhotoManager, PhotoManager>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton(new DbConnectionOptions { ConnectionString = Path.Combine(FileSystem.AppDataDirectory, "reminder.db") });
            services.AddSingleton<INotificationServices, NotificationServices>();
        }
    }
}
