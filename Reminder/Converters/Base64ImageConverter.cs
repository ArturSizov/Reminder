using System.Globalization;

namespace Reminder.Converters
{
    public class Base64ImageConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var base64 = value as string;

            if (value == null || base64 == null) 
                return "ic_anonymous.png";

            return ImageSource.FromStream(() => new MemoryStream(System.Convert.FromBase64String(base64)));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
