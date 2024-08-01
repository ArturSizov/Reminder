using CommunityToolkit.Maui.Core;
using System.Globalization;

namespace Reminder.Converters
{
    public class StatusBarStyleConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not Color color)
                return Colors.Black;

            if (color.ToString() == Color.Parse("#141414").ToString())
                return StatusBarStyle.LightContent;

            else
                return StatusBarStyle.DarkContent;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
