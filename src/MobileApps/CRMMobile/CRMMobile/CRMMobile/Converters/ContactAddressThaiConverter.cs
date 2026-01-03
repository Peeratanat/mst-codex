using IO.Swagger.Model;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace CRMMobile.Converters
{
    public class ContactAddressThaiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fullAddress = string.Empty;
            var address = value as ContactAddressDTO;
            if (address == null)
                return string.Empty;

            if (!string.IsNullOrEmpty(address.HouseNoTH))
                fullAddress += "บ้านเลขที่ " + address.HouseNoTH + "  ";
            if (!string.IsNullOrEmpty(address.VillageTH))
                fullAddress += "หมู่บ้าน/อาคาร " + address.VillageTH + "  ";
            if (!string.IsNullOrEmpty(address.MooTH))
                fullAddress += "หมู่ที่ " + address.MooTH + "  ";
            if (!string.IsNullOrEmpty(address.SoiTH))
                fullAddress += "ซอย " + address.SoiTH + "  ";
            if (!string.IsNullOrEmpty(address.RoadTH))
                fullAddress += "ถนน " + address.RoadTH + "  ";
            if (address.SubDistrict != null)
                fullAddress += "แขวง/ตำบล " + address.SubDistrict.NameTH + "  ";
            if (address.District != null)
                fullAddress += "เขต/อำเภอ" + address.District.NameTH + "  ";
            if (address.Province != null)
                fullAddress += "จังหวัด " + address.Province.NameTH + "  ";

            fullAddress += address.PostalCode;
            return fullAddress;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}