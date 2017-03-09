using System;
using Windows.UI.Xaml.Data;

namespace Brewery.Converter
{
    class DateTimeToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dateTimeValue = value as DateTime? ?? new DateTime();
            return dateTimeValue.ToString("H:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}