using CRMMobile.Helper;
using IO.Swagger.Model;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace CRMMobile.Converters
{
    public class ContactAddressEngConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var address = value as ContactAddressDTO;
            if (address == null)
                return string.Empty;

            var fullAddress = new StringBuilder();

            if (!string.IsNullOrEmpty(address.HouseNoEN))
                fullAddress.Append(address.HouseNoEN);
            if (!string.IsNullOrEmpty(address.VillageEN))
                fullAddress.Append("Village/Building " + address.VillageEN + "  ");
            if (!string.IsNullOrEmpty(address.MooEN))
                fullAddress.Append("Moo " + address.MooEN + "  ");
            if (!string.IsNullOrEmpty(address.SoiEN))
                fullAddress.Append("Soi " + address.SoiEN + "  ");
            if (!string.IsNullOrEmpty(address.RoadTH))
                fullAddress.Append(address.RoadTH + "Road" + "  ");
            if (address.SubDistrict != null)
                fullAddress.Append(address.SubDistrict.NameEN + "Sub-District" + "  ");
            if (address.District != null)
                fullAddress.Append(address.District.NameEN + "District" + "  ");
            if (address.Province != null)
                fullAddress.Append(address.Province.NameEN + "Province" + "  ");

            fullAddress.Append(address.PostalCode);

            string result = fullAddress.ToString();
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}