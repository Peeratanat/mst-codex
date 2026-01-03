using IO.Swagger.Model;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace CRMMobile.Converters
{
    public class ContactAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var address = value as ContactAddressDTO;
            return address.HouseNoTH + address.MooTH +
                address.VillageTH + address.SoiTH +
                address.RoadTH + address.SubDistrict.NameTH + address.District.NameTH +
                address.Province.NameTH + address.PostalCode;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}