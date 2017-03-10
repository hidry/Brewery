using System;
using Windows.UI.Xaml.Data;

namespace Brewery.Converter
{
    class DoubleToTemperatureStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var valueDouble = value as double? ?? 0.0;
            return $"{Math.Round(valueDouble, 2)} °C";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}