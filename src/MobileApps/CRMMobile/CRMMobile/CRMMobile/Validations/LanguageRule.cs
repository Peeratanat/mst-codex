using System.Text;
using System.Text.RegularExpressions;

namespace CRMMobile.Validations
{
    public class LanguageRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool IsThai { get; set; }
        public bool IsEng { get; set; }
        public bool IsNumber { get; set; }
        public bool IsMaxLength { get; set; }
        public int MaxLength { get; set; }

        public bool Check(T value)
        {
            var _value = value as string;

            if (string.IsNullOrEmpty(_value))
                return true;

            StringBuilder sb = new StringBuilder();
            sb.Append("^[");

            if (IsThai)
                sb.Append("\u0E00-\u0E7F");
            if (IsEng)
                sb.Append("a-zA-Z");

            sb.Append(" ");
            if (IsNumber)
                sb.Append("0-9");

            sb.Append(" .");
            if (IsMaxLength)
            {
                sb.Append("{0,");
                sb.Append(MaxLength.ToString());
                sb.Append("}$");
            }
            else
            {
                sb.Append("]+$");
            }

            return Regex.IsMatch(_value, sb.ToString());
        }
    }
}