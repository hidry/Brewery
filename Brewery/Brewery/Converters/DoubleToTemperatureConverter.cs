using System;
using Windows.UI.Xaml.Data;

namespace Brewery.Converters
{
    class DoubleToTemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var doubleValue = (double) value;
            return $"{Math.Round(doubleValue, 1)} °C";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}