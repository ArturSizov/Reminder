using System.Globalization;

namespace Reminder.Converters
{
    public class EventInformationTextConverter : IMultiValueConverter
    {
        int[] age = [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 95, 100];
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            string? text = string.Empty;

            if (value[0] == null || value[1] == null)
                return text;

            var days = System.Convert.ToInt32(value[1]);

            var date = (DateTime)value[0];

            var current = DateTime.Today;

            var agePerson = current.Year - date.Year;

            if (date.Date > current.AddYears(-agePerson)) agePerson--;

            if (days == 0)
            {
                foreach (var a in age)
                {
                    if (agePerson == a)
                    {
                        text = SDK.Base.Properties.Resource.Anniversary;
                        break;
                    }
                    else text = SDK.Base.Properties.Resource.TodayBirthday;
                }
            }
            else
            {
                foreach (var a in age)
                {
                    if (agePerson + 1 == a)
                    {
                        text = SDK.Base.Properties.Resource.DaysAnniversary;
                        break;
                    }
                    else text = SDK.Base.Properties.Resource.DaysBirthday;
                }
            }

            return text;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
