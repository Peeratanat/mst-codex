using System;
using System.Globalization;
using Xamarin.Forms;

namespace CRMMobile.Converters
{
    public class IsBusyConverter : IValueConverter
    {
        public IsBusyConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NegationConverter : IsBusyConverter
    {
    }
}