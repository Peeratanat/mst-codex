using System;
using System.ComponentModel;

namespace Database.Models
{
    public class ExtensionAttributes
    {
        [AttributeUsage(AttributeTargets.All)]
        public class DeviceInformationAttribute : DescriptionAttribute
        {
            public DeviceInformationAttribute(string description, string value)
            {
                FieldName = description;
                DBColumnName = value;
            }

            public string FieldName { get; set; }
            public string DBColumnName { get; set; }
        }        
    }  
}
