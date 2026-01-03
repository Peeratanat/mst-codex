using System;
using System.Globalization;
using Xamarin.Forms;

namespace CRMMobile.Converters
{
    public class IsMainEmailConverter : IValueConverter
    {
        public IsMainEmailConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isMain = value as bool?;
            if (!isMain.HasValue)
                return string.Empty;
            if (!isMain.Value)
                return string.Empty;

            return "(อิเมล์หลัก)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}