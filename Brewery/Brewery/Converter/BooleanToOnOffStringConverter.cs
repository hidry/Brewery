using System;
using Windows.UI.Xaml.Data;

namespace Brewery.Converter
{
    class BooleanToOnOffStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var valueBoolean = value as bool? ?? false;
            return valueBoolean ? "An" : "Aus"; // todo: Resource-File
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}