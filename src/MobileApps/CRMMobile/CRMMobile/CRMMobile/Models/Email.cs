using CRMMobile.Validations;
using Prism.Mvvm;
using System;

namespace CRMMobile.Models
{
    public class Email : BindableBase
    {
        public Email()
        {
            Id = Guid.NewGuid();
            InitValidation();
        }

        private Guid id;

        public Guid Id
        {
            get => id;
            set { SetProperty(ref id, value); }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; RaisePropertyChanged(nameof(IsSelected)); }
        }

        private ValidationObject<string> emailAddress;

        public ValidationObject<string> EmailAddress
        {
            get => emailAddress;
            set { SetProperty(ref emailAddress, value); }
        }

        //private ValidationObject<string> emailAddress2;
        //public ValidationObject<string> EmailAddress2 {
        //    get => emailAddress2;
        //    set { SetProperty(ref emailAddress2, value);}
        //}

        private void InitValidation()
        {
            EmailAddress = new ValidationObject<string>();
            EmailAddress.ValueChanged += EmailAddress2_ValueChanged;
            EmailAddress.Validations.Add(new EmailRule<string>() { ValidationMessage = "กรุณากรอกอีเมลล์ให้ถูกต้อง" });
        }

        private void EmailAddress2_ValueChanged(object sender, EventArgs e)
        {
            bool isValid = EmailAddress.Validate();
        }
    }
}