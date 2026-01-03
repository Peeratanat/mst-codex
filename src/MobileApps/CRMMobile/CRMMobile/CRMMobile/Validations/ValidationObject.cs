using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMMobile.Validations
{
    public class ValidationObject<T> : BindableBase, IValidity
    {
        public event EventHandler<EventArgs> ValueChanged;

        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;

        private bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                RaisePropertyChanged(nameof(Errors));
            }
        }

        private T _value;

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;

                RaisePropertyChanged(nameof(Value));
                ValueChanged?.Invoke(this, EventArgs.Empty);
                var isThaiCharactersRule = _validations.FirstOrDefault(t => t.GetType() == typeof(IsThaiCharactersRule<string>));
                if (isThaiCharactersRule != null && !string.IsNullOrEmpty(value as string))
                    Validate();

                var isTelephoneNoRule = _validations.FirstOrDefault(t => t.GetType() == typeof(IsNumberRule<string>));
                if (isTelephoneNoRule != null && !string.IsNullOrEmpty(value as string))
                    Validate();

                var isLanguageRule = _validations.FirstOrDefault(t => t.GetType() == typeof(LanguageRule<string>));
                if (isLanguageRule != null && !string.IsNullOrEmpty(value as string))
                    Validate();

                var isCitizenOrPassportRule = _validations.FirstOrDefault(t => t.GetType() == typeof(CitizenOrPassportRule<string>));
                if (isCitizenOrPassportRule != null)
                    Validate();
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(nameof(IsValid)); //SetProperty(ref _isValid, value);
            }
        }

        public ValidationObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

        public void Clear()
        {
            Errors.Clear();
            //IsValid = false;
        }
    }
}