using System;
using System.Globalization;
using System.Windows.Data;

namespace Pellared.SalaryBook.Converters
{
    [ValueConversion(typeof(DateTime), typeof(int))]
    public class DateTimeToYearsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime))
                return null;
            var dateTime = (DateTime)value;

            TimeSpan span = DateTime.Now.Subtract(dateTime);
            int years = (int)(span.Days / 365.25); // leap years included
            return years;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}