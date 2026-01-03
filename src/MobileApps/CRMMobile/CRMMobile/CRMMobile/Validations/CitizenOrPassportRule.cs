using System.Text.RegularExpressions;

namespace CRMMobile.Validations
{
    public class CitizenOrPassportRule<T> : IValidationRule<T>
    {
        public bool IsCitizen { get; set; }
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var _value = value as string;
            if (string.IsNullOrEmpty(_value))
                return false;

            string pattern = "";

            if (IsCitizen)
            {
                pattern = @"^[0-9]{0,13}$";
            }
            else
            {
                pattern = @"^[a-zA-Z0-9 ]+$";
            }

            return Regex.IsMatch(_value, pattern);
        }
    }
}