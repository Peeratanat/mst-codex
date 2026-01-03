namespace CRMMobile.Validations
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Check(T value);
    }

    public interface IValidity
    {
        bool IsValid { get; set; }
    }
}