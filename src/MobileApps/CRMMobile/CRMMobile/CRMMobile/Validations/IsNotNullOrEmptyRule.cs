using System.Text.RegularExpressions;

namespace CRMMobile.Validations
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
                return false;

            var str = value as string;
            return !string.IsNullOrEmpty(str);
        }
    }

    public class IsThaiCharactersRule<T> : IValidationRule<T>
    {
        public readonly string ThaiLetters = @"^[ก-๙]+$";
        public readonly string EngLetters = @"^[a-zA-Z]";

        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            var _value = value as string;

            if (string.IsNullOrEmpty(_value))
                return true;

            Regex r = new Regex(ThaiLetters);
            return r.IsMatch(_value);
        }
    }
}