using System;
using System.Globalization;
using Xamarin.Forms;

namespace CRMMobile.Converters
{
    public class DashesCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                var val = value as string;
                return string.IsNullOrEmpty(val) ? "-" : value;
            }
            else
            {
                if (value == null)
                    return "-";
                else
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}