using System;
using System.Globalization;
using Xamarin.Forms;

namespace CRMMobile.Converters
{
    public class DisplayContactID : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as string;
            return !string.IsNullOrEmpty(val) ? $" (Contact ID : {value})" : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}