using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;
using DevExpress.Maui;
using DevExpress.Maui.Core;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using Reminder.DataAccessLayer.DAO;
using Reminder.IoCModules;
using SDK.Base.Abstractions;
using SDK.Base.Auxiliary;

namespace Reminder
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            ThemeManager.Theme = new Theme(Color.FromArgb("#ACACAC"));

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseServices()
                .UseMauiCommunityToolkit()
                .UseDevExpress(useLocalization: false)
                .UseDevExpressCharts()
                .UseDevExpressCollectionView()
                .UseDevExpressControls()
                .UseDevExpressEditors()
                .UseDevExpressDataGrid()
                .UseDevExpressScheduler()
                .UseFFImageLoading()
                .UseLocalNotification()
                .UseUserDialogs(true, () =>
                {
#if ANDROID
                    var fontFamily = "OpenSans-Regular.ttf";
#else
                    var fontFamily = "OpenSansRegular";
#endif
                    AlertConfig.DefaultMessageFontFamily = fontFamily;
                    AlertConfig.DefaultUserInterfaceStyle = UserInterfaceStyle.Dark;
                    AlertConfig.DefaultPositiveButtonTextColor = Colors.Purple;
                    ConfirmConfig.DefaultMessageFontFamily = fontFamily;
                    ActionSheetConfig.DefaultMessageFontFamily = fontFamily;
                    ToastConfig.DefaultMessageFontFamily = fontFamily;
                    SnackbarConfig.DefaultMessageFontFamily = fontFamily;
                    HudDialogConfig.DefaultMessageFontFamily = fontFamily;
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Inkfree.ttf", "Inkfree");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            //Initialization of view models root container
            RootContainer.Container.Initialize(app.Services);

            return app;
        }
    }
}
