using System.Text.RegularExpressions;

namespace CRMMobile.Validations
{
    public class IsNumberRule<T> : IValidationRule<T>
    {
        public readonly string TelephoneFormate = @"^[0-9]";
        public string ValidationMessage { get; set; }
        public int MaxLenght { get; set; }
        private string pattern;

        public bool Check(T value)
        {
            if (MaxLenght > 0)
            {
                pattern = $"{TelephoneFormate}" + "{0," + $"{MaxLenght}" + "}$";
            }
            else
            {
                pattern = $"{TelephoneFormate}+$";
            }

            var _value = value as string;
            if (string.IsNullOrEmpty(_value))
                return true;
            Regex r = new Regex(pattern);
            return r.IsMatch(_value);
        }
    }
}