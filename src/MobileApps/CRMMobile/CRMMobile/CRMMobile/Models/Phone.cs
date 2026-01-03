using CRMMobile.Validations;
using IO.Swagger.Model;
using Prism.Mvvm;
using System;

namespace CRMMobile.Models
{
    public class Phone : BindableBase
    {
        public Phone()
        {
            Id = Guid.NewGuid();
            RemoveVisible = true;
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
            set
            {
                isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }

        private bool removeVisible;
        public bool RemoveVisible
        {
            get => removeVisible;
            set
            {
                removeVisible = value;
                RaisePropertyChanged(nameof(RemoveVisible));
            }
        }

        private ValidationObject<string> phoneNumber;

        public ValidationObject<string> PhoneNumber
        {
            get => phoneNumber;
            set { SetProperty(ref phoneNumber, value); }
        }

        private bool canInputPhoneNumberEXT;

        public bool CanInputPhoneNumberEXT
        {
            get => canInputPhoneNumberEXT;
            set { SetProperty(ref canInputPhoneNumberEXT, value); }
        }

        private string phoneNumberEXT;

        public string PhoneNumberEXT
        {
            get => phoneNumberEXT;
            set { SetProperty(ref phoneNumberEXT, value); }
        }

        private MasterCenterDropdownDTO phoneType;

        public MasterCenterDropdownDTO PhoneType
        {
            get => phoneType;
            set { SetProperty(ref phoneType, value); }
        }

        private void InitValidation()
        {
            PhoneNumber = new ValidationObject<string>();
            PhoneNumber.ValueChanged += PhoneNumber_ValueChanged;
            PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกเบอร์โทรศัพท์" });
            PhoneNumber.Validations.Add(new IsNumberRule<string>() { ValidationMessage = "กรุณากรอกเบอร์โทรศัพท์ให้ถูกต้อง" });
        }

        private void PhoneNumber_ValueChanged(object sender, EventArgs e)
        {
            PhoneNumber.Validate();
        }
    }
}