using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Brewery.Converter
{
    class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var valueBoolean = value as bool? ?? false;
            return valueBoolean ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}