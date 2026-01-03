using System;
using System.Globalization;
using CRMMobile.Helper;
using CRMMobile.Models;
using IO.Swagger.Model;
using Xamarin.Forms;

namespace CRMMobile.Converters
{
    public class DisolayFollowUpDueDateConveter : IValueConverter
    {
        public DisolayFollowUpDueDateConveter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            //var isSelect = (bool)value; // as bool;
            var radio = value as RadioModel<LeadActivityStatusDTO>;
            if(radio.IsSelected && radio.Value.LeadActivityFollowUpType.Equals(LeadActivityFollowUpType.FollowUp))
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
