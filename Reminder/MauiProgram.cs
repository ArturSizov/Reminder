using CommunityToolkit.Maui;
using DevExpress.Maui;
using DevExpress.Maui.Core;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Reminder.IoCModules;

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
                .UseDevExpress(useLocalization: true)
                .UseDevExpressCharts()
                .UseDevExpressCollectionView()
                .UseDevExpressControls()
                .UseDevExpressEditors()
                .UseDevExpressDataGrid()
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Inkfree.ttf", "Inkfree");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
