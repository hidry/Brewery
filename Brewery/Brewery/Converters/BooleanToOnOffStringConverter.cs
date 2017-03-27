using System;
using Windows.UI.Xaml.Data;

namespace Brewery.Converters
{
    class BooleanToOnOffStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var booleanValue = (bool) value;
            if (booleanValue)
            {
                return "An";
            }
            else
            {
                return "Aus";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}