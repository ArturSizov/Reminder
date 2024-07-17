using System.Globalization;

namespace Reminder.Converters
{
    public class CornerRadiusConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;

            return value switch
            {
                double v => (int)v / 2,

                _ => int.TryParse(value.ToString(), out var v) ? v / 2 : 0
            };

        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
