namespace CRMMobile.Validations
{
    public class IsObjectNull<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
                return false;

            return true;
        }
    }
}