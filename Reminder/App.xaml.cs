using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;


namespace Reminder
{
    public partial class App
    {
        public App(AppShell shell)
        {
            InitializeComponent();

            MainPage = shell;
            //Solves the problem with displaying the virtual keyboard on Android

#if ANDROID
            Current?.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

#endif
        }
    }
}
