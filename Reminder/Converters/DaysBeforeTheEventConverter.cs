using System.Globalization;

namespace Reminder.Converters
{
    internal class DaysBeforeTheEventConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;

            var birthday = (DateTime)value;

            var today = DateTime.Today;

            var date = birthday.AddYears(today.Year - birthday.Year);

            if (date < today)
                date = date.AddYears(1);

            return (date - today).Days;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
