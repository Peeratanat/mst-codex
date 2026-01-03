using System.Text.RegularExpressions;

namespace CRMMobile.Validations
{
    public class EmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public string emailPattern = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        public bool Check(T value)
        {
            if (value == null)
                return false;

            var _value = value as string;
            return Regex.IsMatch(_value, emailPattern, RegexOptions.IgnoreCase);
            //try
            //{
            //    MailAddress m = new MailAddress(_value);
            //    return true;
            //}
            //catch (FormatException)
            //{
            //    return false;
            //}
        }
    }
}