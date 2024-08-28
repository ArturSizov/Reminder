using System.Globalization;

namespace Reminder.Converters
{
    public class AgeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value == null) 
                return 0;

            var date = (DateTime)value;

            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (date.Year * 100 + date.Month) * 100 + date.Day;

            return (a - b) / 10000;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
