using System.Globalization;

namespace Reminder.Converters
{
    public class LabelMarginConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var thickness = new Thickness(0);

            if (value is not string str)
                return thickness;

            if (string.IsNullOrEmpty(str))
                return thickness;
            else
                return new Thickness(5, 0, 0, 0);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
