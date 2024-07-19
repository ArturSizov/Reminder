using Reminder.Resources.Themes;

namespace Reminder.Managers
{
    public static class ThemeManager
    {
        private static readonly IDictionary<string, ResourceDictionary> _themes = new Dictionary<string, ResourceDictionary>
        {
            [nameof(Light)] = new Light(),
            [nameof(Dark)] = new Dark()
        };

        static ThemeManager()
        {
            Application.Current!.RequestedThemeChanged += Current_RequestedThemeChanged;
        }

        private static void Current_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            if(e.RequestedTheme == AppTheme.Dark)
                SetTheme(nameof(Dark));
            else 
                SetTheme(nameof(Light));
        }

        public static string SelectedTheme { get; set; } = nameof(Light);

        public static void SetTheme(string themeName)
        {
            if(SelectedTheme == themeName) 
                return;

            var theme = _themes[themeName];

            Application.Current?.Resources.MergedDictionaries.Clear();
            Application.Current?.Resources.MergedDictionaries.Add(theme);

            SelectedTheme = themeName;
        }
    }
}
