using System.Globalization;

namespace Reminder.Converters
{
    public class RowToBrushDaysBirthdayConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || (string?)value == string.Empty)
                return Colors.Black;

            var days = System.Convert.ToInt32(value);

            Color? color;

            if (days == 0) return color = Colors.Transparent;

            else if (days <= 10) color = Colors.Red;

            else if (days <= 50) color = Colors.SaddleBrown;

            else color = Colors.Black;

            return color;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
