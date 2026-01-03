using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DateTimeExtensions
{
    public class JsonDateTimeConverter : DateTimeConverterBase
    {
        private string _dateFormat = "yyyy-MM-dd HH:mm:ss";
        private string _culture = "en-US";

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                var dateValue = reader.Value.ToString();
                DateTime dateResult;
                DateTime.TryParseExact(dateValue, _dateFormat, null, DateTimeStyles.AssumeLocal, out dateResult);
                return dateResult;
            }
            else
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var enUS = new CultureInfo(_culture);
            writer.WriteValue(((DateTime)value).ToString(_dateFormat, enUS));
        }
    }
}
