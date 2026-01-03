using System;
using System.Globalization;

namespace CRMMobile.Converters
{
    public class DisplayProjectConverter : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IO.Swagger.Model.ProjectDTO)
            {
                var project = value as IO.Swagger.Model.ProjectDTO;
                if (project == null)
                {
                    return "-";
                }
                else
                {
                    return project.ProjectNo + "-" + project.ProjectNameTH;
                }
            }
            else
                return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}