using CRMMobile.Helper;
using CRMMobile.Models;
using CRMMobile.Validations;
using CRMMobile.Views.Popup;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRMMobile.ViewModels
{
    public class ContactFormViewModel : ViewModelBase
    {
        public ContactFormViewModel(INavigationService navigationService) : base(navigationService)
        {
            InitValidation();
            RaisePropertyChanged("IsCoperation");
            CommandCoperation = new DelegateCommand(() =>
            {
                IsCoperation = !IsCoperation;
                IsPerson = !IsCoperation;
                OnContactTypeSelected?.Invoke(null, IsPerson);
            });

            CommandPerson = new DelegateCommand(() =>
            {
                IsPerson = !IsPerson;
                IsCoperation = !IsPerson;
                OnContactTypeSelected?.Invoke(null, IsPerson);
            });

            CommandPrefixsThai = new DelegateCommand<string>(async (str) =>
            {
                var result = await GetMasterCenterData(MasterCenterKey.ContactTitleTH, str);
                PrefixsThai = new ObservableCollection<MasterCenterDropdownDTO>(result);
            });

            CommandPrefixsEng = new DelegateCommand<string>(async (str) =>
            {
                var result = await GetMasterCenterData(MasterCenterKey.ContactTitleEN, str);
                PrefixsEng = new ObservableCollection<MasterCenterDropdownDTO>(result);
            });

            CommandNations = new DelegateCommand<string>(async (str) =>
            {
                var result = await GetMasterCenterData(MasterCenterKey.National, str);
                Nations = new ObservableCollection<MasterCenterDropdownDTO>(result);
            });

            Phones = new ObservableCollection<Phone>();
            Emails = new ObservableCollection<Email>();
            PrefixsThai = new ObservableCollection<MasterCenterDropdownDTO>();
            PrefixsEng = new ObservableCollection<MasterCenterDropdownDTO>();
            PhoneTypes = new ObservableCollection<MasterCenterDropdownDTO>();
            Nations = new ObservableCollection<MasterCenterDropdownDTO>();
            Gender = new CheckBoxGroup();
            Gender.OnItemSelectedChange += (s, e) => { SelectGender.Value = e.Value as MasterCenterDropdownDTO; SelectGender.Validate(); };
        }

        #region Services


        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        [Unity.Dependency]
        public IProjectsApi ProjectsApi { get; set; }

        [Unity.Dependency]
        public IContactsApi ContactsApi { get; set; }

        #endregion Services

        public EventHandler<EventArgs> OnLoadedEvent { get; set; }
        public event EventHandler<bool> OnContactTypeSelected;

        public DelegateCommand CommandCoperation { get; set; }
        public DelegateCommand CommandPerson { get; set; }
        public DelegateCommand<string> CommandNations { get; set; }
        public DelegateCommand<string> CommandPrefixsThai { get; set; }
        public DelegateCommand<string> CommandPrefixsEng { get; set; }
        public DelegateCommand CreateCommand => new DelegateCommand(CreateOrUpdateContract);

        public DelegateCommand<Phone> PhoneCheckCommand => new DelegateCommand<Phone>(value =>
        {
            foreach (var item in Phones)
            {
                if (item.Id == value.Id)
                {
                    item.IsSelected = true;
                }
                else
                {
                    item.IsSelected = false;
                }
            }
        });

        public DelegateCommand CommandAddPhone => new DelegateCommand(() =>
        {
            Phones.Add(new Phone());
            if (Phones.Count == 1)
            {
                var _phone = Phones.FirstOrDefault();
                _phone.IsSelected = true;
            }
        });

        public DelegateCommand<Phone> CommandRemovePhone => new DelegateCommand<Phone>(value =>
        {
            if (value.IsSelected)
            {
                var index = Phones.IndexOf(value);
                if (index == 0)
                {
                    Phones.RemoveAt(0);
                    if (Phones.Count > 0)
                        Phones.FirstOrDefault().IsSelected = true;
                }
            }
            else
            {
                Phones.Remove(value);
            }
        });

        public DelegateCommand<Email> EmailCheckCommand => new DelegateCommand<Email>(value =>
        {
            foreach (var item in Emails)
            {
                if (item.Id == value.Id)
                    item.IsSelected = true;
                else
                    item.IsSelected = false;
            }
        });

        public DelegateCommand CommandAddEmail => new DelegateCommand(() =>
        {
            Emails.Add(new Email());

            if (Emails.Count == 1)
            {
                var email = Emails.FirstOrDefault();
                email.IsSelected = true;
            }
        });

        public DelegateCommand<Email> CommandRemoveEmail => new DelegateCommand<Email>(value =>
        {
            if (value.IsSelected)
            {
                var index = Emails.IndexOf(value);
                if (index == 0)
                {
                    Emails.RemoveAt(0);
                    if (Emails.Count > 0)
                        Emails.FirstOrDefault().IsSelected = true;
                }
            }
            else
            {
                Emails.Remove(value);
            }
        });

        #region Form

        public Guid ContactId { get; private set; }
        public FormMode FormMode { get; private set; }
        public Guid? VisitorId { get; private set; }
        public string CitizenPlaceholder { get; private set; }

        /// <summary>
        /// True = คือบุคคลธรรมดา, False = นิติบุคคล
        /// </summary>
        private bool isPerson;

        public bool IsPerson
        {
            get { return isPerson; }
            set { SetProperty(ref isPerson, value); }
        }

        private bool isCoperation;

        public bool IsCoperation
        {
            get { return isCoperation; }
            set { SetProperty(ref isCoperation, value); }
        }

        private MasterCenterDropdownDTO person;

        public MasterCenterDropdownDTO Person
        {
            get { return person; }
            set { SetProperty(ref person, value); }
        }

        private MasterCenterDropdownDTO coperation;

        public MasterCenterDropdownDTO Coperation
        {
            get { return coperation; }
            set { SetProperty(ref coperation, value); }
        }

        private ObservableCollection<Phone> phones;

        public ObservableCollection<Phone> Phones
        {
            get { return phones; }
            set { SetProperty(ref phones, value); }
        }

        private ObservableCollection<Email> emails;

        public ObservableCollection<Email> Emails
        {
            get { return emails; }
            set { SetProperty(ref emails, value); }
        }

        private ObservableCollection<MasterCenterDropdownDTO> prefixsThai;

        public ObservableCollection<MasterCenterDropdownDTO> PrefixsThai
        {
            get { return prefixsThai; }
            set { SetProperty(ref prefixsThai, value); }
        }

        private ObservableCollection<MasterCenterDropdownDTO> prefixsEng;

        public ObservableCollection<MasterCenterDropdownDTO> PrefixsEng
        {
            get { return prefixsEng; }
            set { SetProperty(ref prefixsEng, value); }
        }

        private ObservableCollection<MasterCenterDropdownDTO> nations;

        public ObservableCollection<MasterCenterDropdownDTO> Nations
        {
            get { return nations; }
            set { SetProperty(ref nations, value); }
        }

        private ObservableCollection<MasterCenterDropdownDTO> phoneTypes;

        public ObservableCollection<MasterCenterDropdownDTO> PhoneTypes
        {
            get { return phoneTypes; }
            set { SetProperty(ref phoneTypes, value); }
        }

        private ValidationObject<string> citizenId;

        public ValidationObject<string> CitizenId
        {
            get => citizenId;
            set { SetProperty(ref citizenId, value); }
        }

        private ValidationObject<DateTime?> citizenExpire;

        public ValidationObject<DateTime?> CitizenExpire
        {
            get => citizenExpire;
            set { SetProperty(ref citizenExpire, value); }
        }

        private ValidationObject<DateTime?> birthDate;

        public ValidationObject<DateTime?> BirthDate
        {
            get => birthDate;
            set { SetProperty(ref birthDate, value); }
        }

        private ValidationObject<string> firstname;

        public ValidationObject<string> Firstname
        {
            get => firstname;
            set { SetProperty(ref firstname, value); }
        }

        private ValidationObject<MasterCenterDropdownDTO> prefix;

        public ValidationObject<MasterCenterDropdownDTO> Prefix
        {
            get => prefix;
            set { SetProperty(ref prefix, value); }
        }

        private ValidationObject<string> prefixExt;

        public ValidationObject<string> PrefixExt
        {
            get => prefixExt;
            set { SetProperty(ref prefixExt, value); }
        }

        private bool prefixExtEnable;

        public bool PrefixExtEnable
        {
            get => prefixExtEnable;
            set { SetProperty(ref prefixExtEnable, value); }
        }

        private ValidationObject<string> lastname;

        public ValidationObject<string> Lastname
        {
            get => lastname;
            set { SetProperty(ref lastname, value); }
        }

        private ValidationObject<string> middleName;

        public ValidationObject<string> Middlename
        {
            get => middleName;
            set { SetProperty(ref middleName, value); }
        }

        private ValidationObject<MasterCenterDropdownDTO> selectGender;

        public ValidationObject<MasterCenterDropdownDTO> SelectGender
        {
            get => selectGender;
            set { SetProperty(ref selectGender, value); }
        }

        private string nickname;

        public string Nickname
        {
            get => nickname;
            set { SetProperty(ref nickname, value); }
        }

        private string weChat;

        public string WeChat
        {
            get => weChat;
            set { SetProperty(ref weChat, value); }
        }

        private string whatApp;

        public string WhatApp
        {
            get => whatApp;
            set { SetProperty(ref whatApp, value); }
        }

        private string lineId;

        public string LineId
        {
            get => lineId;
            set { SetProperty(ref lineId, value); }
        }

        private ValidationObject<MasterCenterDropdownDTO> nation;

        public ValidationObject<MasterCenterDropdownDTO> Nation
        {
            get => nation;
            set { SetProperty(ref nation, value); }
        }

        private ValidationObject<MasterCenterDropdownDTO> prefixEN;

        public ValidationObject<MasterCenterDropdownDTO> PrefixEN
        {
            get => prefixEN;
            set { SetProperty(ref prefixEN, value); }
        }

        private ValidationObject<string> prefixENExt;

        public ValidationObject<string> PrefixENExt
        {
            get => prefixENExt;
            set { SetProperty(ref prefixENExt, value); }
        }

        private bool prefixENEnable;

        public bool PrefixENEnable
        {
            get => prefixENEnable;
            set { SetProperty(ref prefixENEnable, value); }
        }

        private ValidationObject<string> firstnameEN;

        public ValidationObject<string> FirstnameEN
        {
            get => firstnameEN;
            set { SetProperty(ref firstnameEN, value); }
        }

        private ValidationObject<string> lastnameEN;

        public ValidationObject<string> LastnameEN
        {
            get => lastnameEN;
            set { SetProperty(ref lastnameEN, value); }
        }

        private ValidationObject<string> middlenameEN;

        public ValidationObject<string> MiddlenameEN
        {
            get => middlenameEN;
            set { SetProperty(ref middlenameEN, value); }
        }

        //Company

        private ValidationObject<string> companyname;

        public ValidationObject<string> Companyname
        {
            get => companyname;
            set { SetProperty(ref companyname, value); }
        }

        private ValidationObject<string> taxId;

        public ValidationObject<string> TaxId
        {
            get => taxId;
            set { SetProperty(ref taxId, value); }
        }

        private ValidationObject<string> telephoneCompany;

        public ValidationObject<string> TelephoneCompany
        {
            get => telephoneCompany;
            set { SetProperty(ref telephoneCompany, value); }
        }

        private string telephoneCompanyExt;

        public string TelephoneCompanyExt
        {
            get => telephoneCompanyExt;
            set { SetProperty(ref telephoneCompanyExt, value); }
        }

        private string contactName;

        public string ContactName
        {
            get => contactName;
            set { SetProperty(ref contactName, value); }
        }

        private string contactLastName;

        public string ContactLastName
        {
            get => contactLastName;
            set { SetProperty(ref contactLastName, value); }
        }

        private ValidationObject<string> companyNameEN;

        public ValidationObject<string> CompanyNameEN
        {
            get => companyNameEN;
            set { SetProperty(ref companyNameEN, value); }
        }

        private bool isVip;

        public bool IsVip
        {
            get => isVip;
            set { SetProperty(ref isVip, value); }
        }

        private bool canChangeContactType;

        public bool CanChangeContactType
        {
            get => canChangeContactType;
            set { SetProperty(ref canChangeContactType, value); }
        }

        private CheckBoxGroup gender;

        public CheckBoxGroup Gender
        {
            get => gender;
            set { SetProperty(ref gender, value); }
        }

        #endregion Form

        #region InitValidation object data

        private void InitValidation()
        {
            Firstname = new ValidationObject<string>();
            Lastname = new ValidationObject<string>();
            Phones = new ObservableCollection<Phone>();
            Prefix = new ValidationObject<MasterCenterDropdownDTO>();
            Prefix.ValueChanged += Prefix_ValueChanged;
            PrefixExt = new ValidationObject<string>();
            SelectGender = new ValidationObject<MasterCenterDropdownDTO>();
 
            Nation = new ValidationObject<MasterCenterDropdownDTO>();
            Nation.ValueChanged += Nation_ValueChanged;
            CitizenId = new ValidationObject<string>();
            CitizenExpire = new ValidationObject<DateTime?>();
            BirthDate = new ValidationObject<DateTime?>();
            Middlename = new ValidationObject<string>();

            FirstnameEN = new ValidationObject<string>();
            LastnameEN = new ValidationObject<string>();
            PrefixEN = new ValidationObject<MasterCenterDropdownDTO>();
            PrefixEN.ValueChanged += PrefixEN_ValueChanged;
            PrefixENExt = new ValidationObject<string>();
            MiddlenameEN = new ValidationObject<string>();

            Firstname.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกชื่อ" });
            Firstname.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกชื่อด้วยภาษาไทยหรืออังกฤษ", IsThai = true, IsEng = true });

            Lastname.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกนามสกุล" });
            Lastname.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกนามสกุลด้วยภาษาไทยหรืออังกฤษ", IsThai = true, IsEng = true });

            //PrefixExt.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกด้วยคำนำหน้าเพิ่มเติม" });
            PrefixExt.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกด้วยคำนำหน้าเพิ่มเติมด้วยภาษาไทยหรืออังกฤษ", IsThai = true, IsEng = true });

            Middlename.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกด้วยภาษาไทยหรืออังกฤษ", IsThai = true, IsEng = true });

            Prefix.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>() { ValidationMessage = "กรุณาเลือกคำนำหน้าชื่อ" });
            Nation.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>() { ValidationMessage = "กรุณาเลือกสัญชาติ" });
            BirthDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "กรุณากรอกวันเดือนปีเกิด" });
            SelectGender.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>() { ValidationMessage = "กรุณาระบุเพศ" });

            //CitizenId.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกเลขที่บัตรประชาชน" });
            CitizenExpire.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "กรุณากรอกวันหมดอายุ" });

            FirstnameEN.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกชื่อภาษาอังกฤษ" });
            FirstnameEN.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกชื่อด้วยภาษาอังกฤษเท่านั้น", IsEng = true });

            //PrefixENExt.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกด้วยคำนำหน้าเพิ่มเติม" });
            PrefixENExt.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกด้วยคำนำหน้าเพิ่มเติมด้วยภาษาอังกฤษเท่านั้น", IsThai = true, IsEng = true });

            LastnameEN.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกนามสกุลภาษาอังกฤษ" });
            LastnameEN.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกนามสกุลด้วยภาษาอังกฤษเท่านั้", IsEng = true });

            PrefixEN.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>() { ValidationMessage = "กรุณาเลือกคำนำหน้าชื่อด้วยภาษาอังกฤษเท่านั้น" });

            MiddlenameEN.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกด้วยอังกฤษเท่านั้น", IsEng = true });

            Companyname = new ValidationObject<string>();
            CompanyNameEN = new ValidationObject<string>();
            TaxId = new ValidationObject<string>();
            TelephoneCompany = new ValidationObject<string>();

            Companyname.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกชื่อบริษัทด้วยไทยและอังกฤษ" });
            Companyname.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกชื่อบริษัทด้วยไทยและอังกฤษ", IsThai = true, IsEng = true });

            CompanyNameEN.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกชื่อบริษัทด้วยถาษาอังกฤษ" });
            CompanyNameEN.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกชื่อบริษัทด้วยถาษาอังกฤษ", IsEng = true });

            TaxId.Validations.Add(new IsObjectNull<string>() { ValidationMessage = "กรุณากรอกเลขประจำตัวผู้เสียภาษี" });
            TaxId.Validations.Add(new IsNumberRule<string>() { ValidationMessage = "กรุณากรอกเลขประจำตัวผู้เสียภาษี" });

            TelephoneCompany.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกเบอร์โทร" });
            TelephoneCompany.Validations.Add(new IsNumberRule<string>() { ValidationMessage = "กรุณากรอกเบอร์โทรให้ถูกต้อง" });
        }

        private void PrefixEN_ValueChanged(object sender, EventArgs e)
        {
            if (PrefixEN.Value == null) return;
            if (PrefixEN.Value.Key.Equals(PrefixType.Other))
            {
                PrefixENEnable = true;

                var isNullOrEmptyEN = PrefixENExt.Validations.Any(t => t.GetType() == typeof(IsNotNullOrEmptyRule<string>));
                if (!isNullOrEmptyEN)
                    PrefixENExt.Validations.Insert(0, new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกด้วยคำนำหน้าเพิ่มเติม" });

            }
            else
            {
                PrefixENEnable = false;
                PrefixExt.Value = string.Empty;
                PrefixENExt.Value = string.Empty;

                Prefix.IsValid = true;

                var isNullOrEmptyEN = PrefixENExt.Validations.FirstOrDefault(t => t.GetType() == typeof(IsNotNullOrEmptyRule<string>));
                if (isNullOrEmptyEN != null)
                    PrefixENExt.Validations.Remove(isNullOrEmptyEN);

                PrefixENExt.Validate();
            }


            if (Prefix.Value != null && Prefix.Value.Key == PrefixEN.Value.Key) return;
            var item = PrefixsThai.FirstOrDefault(t => t.Key == PrefixEN.Value.Key);
            Prefix.Value = item;
            
        }

        private async void Prefix_ValueChanged(object sender, EventArgs e)
        {
            if (Prefix.Value == null) return;
            if (Prefix.Value.Key.Equals(PrefixType.Other))
            {
                PrefixExtEnable = true;

                var isNullOrEmpty = PrefixExt.Validations.Any(t => t.GetType() == typeof(IsNotNullOrEmptyRule<string>));
                if (!isNullOrEmpty)
                    PrefixExt.Validations.Insert(0 ,new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกด้วยคำนำหน้าเพิ่มเติม" });

                var isNullOrEmptyEN = PrefixENExt.Validations.Any(t => t.GetType() == typeof(IsNotNullOrEmptyRule<string>));
                if (!isNullOrEmptyEN)
                    PrefixENExt.Validations.Insert(0, new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกด้วยคำนำหน้าเพิ่มเติม" });

            }
            else
            {
                PrefixExtEnable = false;
                PrefixExt.Value = string.Empty;
                PrefixENExt.Value = string.Empty;

                var isNullOrEmpty = PrefixExt.Validations.FirstOrDefault(t => t.GetType() == typeof(IsNotNullOrEmptyRule<string>));
                if (isNullOrEmpty != null)
                    PrefixExt.Validations.Remove(isNullOrEmpty);

                var isNullOrEmptyEN = PrefixENExt.Validations.FirstOrDefault(t => t.GetType() == typeof(IsNotNullOrEmptyRule<string>));
                if (isNullOrEmptyEN != null)
                    PrefixENExt.Validations.Remove(isNullOrEmptyEN);

                PrefixENExt.Validate();

                PrefixExt.Validate();
                //PrefixENExt.Validate();
            }

            
            if (PrefixEN.Value != null && Prefix.Value.Key == PrefixEN.Value.Key) return;
            var item = PrefixsEng.FirstOrDefault(t => t.Key == Prefix.Value.Key);
            PrefixEN.Value = item;
        }

        private void Nation_ValueChanged(object sender, EventArgs e)
        {
            if (Nation.Value == null) return;
            var hasCitizenValidation = CitizenId.Validations.FirstOrDefault(t => t.GetType() == typeof(CitizenOrPassportRule<string>));
            if (hasCitizenValidation != null)
                CitizenId.Validations.Remove(hasCitizenValidation);

            if (Nation.Value.Key.Equals(NationalType.ThaiNation))
            {
                CitizenPlaceholder = "เลขที่บัตรบัตรประชาชน*";
                CitizenId.Validations.Add(new CitizenOrPassportRule<string>() { IsCitizen = true, ValidationMessage = "กรุณากรอกเลขที่บัตรให้ถูกต้อง" });
            }
            else
            {
                CitizenPlaceholder = "เลขที่หนังสือเดินทาง*";
                CitizenId.Validations.Add(new CitizenOrPassportRule<string>() { IsCitizen = false, ValidationMessage = "กรุณากรอกเลขที่หนังสือเดินทางให้ถูกต้อง" });
            }

            if (!string.IsNullOrEmpty(CitizenId.Value))
            {
                var isvalid = CitizenId.Validate();
                if(!isvalid)
                    CitizenPlaceholder = CitizenId.Validations[0].ValidationMessage;
            }  

            RaisePropertyChanged(nameof(CitizenPlaceholder));
        }

        private bool Validate()
        {
            string errors = null;
            if (IsPerson)
            {
                if (Nation.Value.Key.Equals(NationalType.ThaiNation))
                {
                    bool isPrefix = Prefix.Validate();
                    bool isFirsname = Firstname.Validate();
                    bool isLastname = Lastname.Validate();
                    bool isNation = Nation.Validate();
                    bool isGender = SelectGender.Validate();

                    //Validate Phone
                    Phones.All(t => { t.PhoneNumber.Validate(); return true; });
                    var notValidPhone = Phones.Where(t => !t.PhoneNumber.IsValid).Any();
                    bool isPrefixExt = true;
                    if (isPrefix && Prefix.Value.Key.Equals(PrefixType.Other))
                        isPrefixExt = PrefixExt.Validate();

                    if (!isPrefix)
                        errors += Prefix.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isPrefixExt)
                        errors += PrefixExt.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isFirsname)
                        errors += Firstname.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isLastname)
                        errors += Lastname.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isNation)
                        errors += Nation.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isGender)
                        errors += SelectGender.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!notValidPhone)
                    {
                        if(Phones.Count == 1)
                        {
                           var phone =  Phones.FirstOrDefault();
                           if(string.IsNullOrEmpty(phone.PhoneNumber.Value))
                           {
                                errors += phone.PhoneNumber.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                            }
                        }
                        else
                        {
                            var inValidPhone = Phones.FirstOrDefault(t => !t.PhoneNumber.IsValid);
                            if (inValidPhone != null)
                                errors += inValidPhone.PhoneNumber.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                        }

                    }

                    if (!string.IsNullOrEmpty(errors))
                        DialogService.DisplayAlertAsync("แจ้งเตือน", errors);
                   
                    return isPrefix && isFirsname && isLastname && isNation && isPrefixExt && notValidPhone;
                }
                else
                {
                    bool isPrefix = Prefix.Validate();
                    bool isFirsname = Firstname.Validate();
                    bool isLastname = Lastname.Validate();
                    bool isNation = Nation.Validate();
                    bool isPrefixEN = PrefixEN.Validate();
                    bool isFirstnameEN = FirstnameEN.Validate();
                    bool isLastnameEN = LastnameEN.Validate();
                    bool isGender = SelectGender.Validate();

                    // Validate Gender
                    // Validate Phone && Email
                    Emails.All(t => { t.EmailAddress.Validate(); return true; });
                    var notValidEmails = Emails.Where(t => !t.EmailAddress.IsValid).Any();
                    Phones.All(t => { t.PhoneNumber.Validate(); return true; });
                    var notValidPhone = Phones.Where(t => !t.PhoneNumber.IsValid).Any();

                    bool isPrefixExt = true;
                    if (isPrefix && Prefix.Value.Key.Equals(PrefixType.Other))
                        isPrefixExt = PrefixExt.Validate();
                    
                    bool isPrefixENExt = true;
                    if (isPrefixEN && PrefixEN.Value.Key.Equals(PrefixType.Other))
                        isPrefixENExt = PrefixENExt.Validate();
                    
                    if (!isPrefix)
                        errors += Prefix.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isGender)
                        errors += SelectGender.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isFirsname)
                        errors += Firstname.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isLastname)
                        errors += Lastname.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isNation)
                        errors += Nation.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isFirstnameEN)
                        errors += FirstnameEN.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isLastnameEN)
                        errors += LastnameEN.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isPrefixEN)
                        errors += PrefixEN.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isPrefixExt)
                        errors += PrefixExt.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (!isPrefixENExt)
                        errors += PrefixENExt.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    if (Emails.Count == 0)
                        errors += "กรุณากรอกอีเมลล์" + Environment.NewLine;
                    if (notValidEmails)
                    {
                        var inValidEmail = Emails.FirstOrDefault(t => !t.EmailAddress.IsValid);
                        if (inValidEmail != null)
                            errors += inValidEmail.EmailAddress.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                    }
                    if (notValidPhone)
                    {
                        if(Phones.Count == 1)
                        {
                            var phone = Phones.FirstOrDefault();
                            if (string.IsNullOrEmpty(phone.PhoneNumber.Value))
                            {
                                errors += phone.PhoneNumber.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                            }
                        }
                        else
                        {
                            var inValidPhone = Phones.FirstOrDefault(t => !t.PhoneNumber.IsValid);
                            if (inValidPhone != null)
                                errors += inValidPhone.PhoneNumber.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                        }
                    }

                    if (!string.IsNullOrEmpty(errors))
                        DialogService.DisplayAlertAsync("แจ้งเตือน", errors);
                   
                    return isPrefix && isFirsname && isLastname &&
                        isNation && isFirstnameEN && isLastnameEN &&
                        isPrefixEN && isPrefixExt && isPrefixENExt && !notValidEmails
                        && !notValidPhone;
                }
                
               
            }
            else
            {
                bool isCompany = Companyname.Validate();
                //bool isComanayEN = CompanyNameEN.Validate();
                bool isTelephoneCompany = TelephoneCompany.Validate();
                //bool isTaxId = TaxId.Validate();

                if (!isCompany)
                    errors += Companyname.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;

                //if (!isComanayEN)
                //    errors += CompanyNameEN.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
                
                if (!isTelephoneCompany)
                    errors += TelephoneCompany.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;
    
                //if (!isTaxId)
                //    errors += TaxId.Validations.FirstOrDefault().ValidationMessage + Environment.NewLine;

                if (!string.IsNullOrEmpty(errors))
                    DialogService.DisplayAlertAsync("แจ้งเตือน", errors);
                // return isCompany && isComanayEN && isTelephoneCompany && isTaxId;

                return isCompany && isTelephoneCompany;
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async override void OnNavigatedModeNew(INavigationParameters parameters)
        {
            base.OnNavigatedModeNew(parameters);
            await Init();
            if (parameters.TryGetValue("ContactId", out Guid contactId))
            {
                ContactId = contactId;
                FormMode = FormMode.Edit;
                CanChangeContactType = false;
                await OnModeEdit();
            }
            else
            {
                CanChangeContactType = true;
                FormMode = FormMode.Create;
                IsPerson = true;

                var tmp = new Phone();
                tmp.IsSelected = true;
                tmp.RemoveVisible = false;
                Phones.Insert(0,tmp);
            }

            if (parameters.TryGetValue("VisitorId", out Guid? visitorId))
                VisitorId = visitorId;
           
        }

        public async Task Init()
        {
            IsBusy = true;
            var result = await Run(() => MasterCentersApi.GetMasterCenterDropdownList("ContactType", null));
            IsBusy = false;
            result = result.OrderBy(t => t.Key).ToList();
            Person = result.FirstOrDefault(t => t.Key == ContactType.Personal);
            Coperation = result.FirstOrDefault(t => t.Key == ContactType.Corporate);

            IsBusy = true;
            var _gender = await Run(() => MasterCentersApi.GetMasterCenterDropdownList("Gender", null));
            IsBusy = false;
            _gender = _gender.OrderBy(t => t.Key).ToList();
            Gender.AddCheckboxValue(_gender);

            var _nations = await GetMasterCenterData(MasterCenterKey.National);
            Nations = new ObservableCollection<MasterCenterDropdownDTO>(_nations);

            var _thaiNation = await GetMasterCenterData(MasterCenterKey.National, "ไทย");
            Nation.Value = _thaiNation.FirstOrDefault();

            var _prefixThai = await GetMasterCenterData(MasterCenterKey.ContactTitleTH);
            PrefixsThai = new ObservableCollection<MasterCenterDropdownDTO>(_prefixThai);

            var _prefixEng = await GetMasterCenterData(MasterCenterKey.ContactTitleEN);
            PrefixsEng = new ObservableCollection<MasterCenterDropdownDTO>(_prefixEng);

            var _phoneType = await GetMasterCenterData(MasterCenterKey.PhoneType);
            PhoneTypes = new ObservableCollection<MasterCenterDropdownDTO>(_phoneType);
        }

        public async Task<List<MasterCenterDropdownDTO>> GetMasterCenterData(string key = null, string filter = null)
            => await Run(() => MasterCentersApi.GetMasterCenterDropdownList(key, filter));
       
        public async void CreateOrUpdateContract()
        {
            if (!Validate())
                return;

            if (!await CheckConnection(isFaultConnectionDisplayAlert: true))
                return;

            var request = CreateContractData();

            try
            {
                if (ContactId == Guid.Empty)
                {
                    IsBusy = true;
                    var contactSimilars = await Run(() => ContactsApi.GetContactSimilar(request));
                    IsBusy = false;

                    
                    if (contactSimilars != null && contactSimilars.ContactSimilars?.Any())
                    {
                        SimilarContact popup = new SimilarContact(contactSimilars.ContactSimilars);
                        popup.OnItemSelected += async (o, e) =>
                        {
                            var item = (ContactSimilarDTO)o;
                            try
                            {
                                await Run(() => ContactsApi.CreateContact(request, item.Contact.Id, VisitorId ?? null));
                            }
                            catch (ApiException ex)
                            {
                                HandleException(ex);
                            }
                        };
                        await DisplayCustomPopup(popup);
                    }
                    else
                    {
                        IsBusy = true; // (ContactDTO input, Guid? similarContactID, Guid? similarLeadID, Guid? fromVisitorID);
                        var result = await Run(() => ContactsApi.CreateContact(request,null, VisitorId ?? null));
                        IsBusy = false;
                        await DisplaySuccessPopup(callBackAction: async () => { await NavigatedBack(); });
                    }
                }
                else
                {
                    IsBusy = true;
                    await Run(() => ContactsApi.EditContact(ContactId, request));
                    IsBusy = false;
                    await DisplaySuccessPopup(callBackAction: async () => { await NavigatedBack(); });
                }
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public ContactDTO CreateContractData()
        {
            var data = new ContactDTO();
            data.WeChat = weChat;
            data.LineID = LineId;
            data.WhatsApp = WhatApp;
            data.IsVIP = IsVip;

            if (IsPerson)
            {
                data.ContactType = Person;

                data.TitleTH = Prefix.Value;
                data.TitleExtTH = PrefixExt.Value;
                data.FirstNameTH = Firstname.Value;
                data.LastNameTH = Lastname.Value;
                data.MiddleNameTH = Middlename.Value;
                data.Nickname = Nickname;
                data.National = Nation.Value;
                data.CitizenIdentityNo = CitizenId.Value;
                data.CitizenExpireDate = CitizenExpire.Value;
                data.Gender = SelectGender.Value; 
                data.BirthDate = BirthDate.Value;
                data.FirstNameEN = FirstnameEN.Value;
                data.LastNameEN = LastnameEN.Value;
                data.MiddleNameEN = MiddlenameEN.Value;
                data.TitleEN = PrefixEN.Value;
                data.TitleExtEN = PrefixENExt.Value;
                if (Nation.Value.Key.Equals(NationalType.ThaiNation))
                    data.IsThaiNationality = true;
            }
            else
            {
                data.ContactType = Coperation;
                data.TaxID = TaxId.Value;
                data.PhoneNumber = TelephoneCompany.Value;
                data.PhoneNumberExt = TelephoneCompanyExt;
                data.ContactFirstName = ContactName;
                data.ContactLastname = ContactLastName;
                data.FirstNameEN = CompanyNameEN.Value;
                data.FirstNameTH = Companyname.Value;
            }

            data.ContactPhones = new List<ContactPhoneDTO>();
            foreach (var item in Phones)
            {
                var tmp = new ContactPhoneDTO()
                {
                    PhoneNumber = item.PhoneNumber.Value,
                    IsMain = item.IsSelected,
                    PhoneType = item.PhoneType,
                    PhoneNumberExt = item.PhoneNumberEXT,
                };
                data.ContactPhones.Add(tmp);
            }

            data.ContactEmails = new List<ContactEmailDTO>();
            foreach (var item in Emails)
            {
                var tmp2 = new ContactEmailDTO()
                {
                    Email = item.EmailAddress.Value,
                    IsMain = item.IsSelected
                };
                data.ContactEmails.Add(tmp2);
            }

            return data;
        }

        public async Task OnModeEdit()
        {
            try
            {
                IsBusy = true;
                var result = await Run(() => ContactsApi.GetContact(ContactId));
                SetupContactForm(result);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public void SetupContactForm(ContactDTO data)
        {
            if (data.ContactType.Key.Equals(ContactType.Personal))
            {
                IsPerson = true;
                Person = data.ContactType;
            }
            else
            {
                IsCoperation = true;
                Coperation = data.ContactType;
            }
            OnContactTypeSelected?.Invoke(this, IsPerson);
            ContactId = data.Id.Value;
            WeChat = data.WeChat;
            LineId = data.LineID;
            WhatApp = data.WhatsApp;
            IsVip = data.IsVIP.Value;

            Prefix.Value = data.TitleTH;
            PrefixExt.Value = data.TitleExtTH;
            Firstname.Value = data.FirstNameTH;
            Lastname.Value = data.LastNameTH;
            Middlename.Value = data.MiddleNameTH;
            Nickname = data.Nickname;
            Nation.Value = data.National;
            CitizenId.Value = data.CitizenIdentityNo;
            CitizenExpire.Value = data.CitizenExpireDate;
            BirthDate.Value = data.BirthDate;

            FirstnameEN.Value = data.FirstNameEN;
            LastnameEN.Value = data.LastNameEN;
            MiddlenameEN.Value = data.MiddleNameEN;
            PrefixEN.Value = data.TitleEN ?? null;
            PrefixENExt.Value = data.TitleExtEN ?? null;

            TaxId.Value = data.TaxID;
            TelephoneCompany.Value = data.PhoneNumber;
            TelephoneCompanyExt = data.PhoneNumberExt;
            ContactName = data.ContactFirstName;
            ContactLastName = data.ContactLastname;
            CompanyNameEN.Value = data.FirstNameEN;
            Companyname.Value = data.FirstNameTH;

            if (data.Gender != null)
            {
                var currentGender = Gender.Checkboxs.FirstOrDefault(t => ((MasterCenterDropdownDTO)t.Value).Key == data.Gender.Key);
                var index = Gender.Checkboxs.IndexOf(currentGender);
                Gender.SetDefault(index);
            }


            foreach (var item in data.ContactPhones)
            {
                var tmp = new Phone();

                tmp.PhoneNumber.Value = item.PhoneNumber;
                tmp.IsSelected = item.IsMain.Value;
                tmp.PhoneType = item.PhoneType;
                tmp.PhoneNumberEXT = item.PhoneNumberExt;
                Phones.Add(tmp);
            }

            foreach (var item in data.ContactEmails)
            {
                var tmp2 = new Email();
                tmp2.EmailAddress.Value = item.Email;
                tmp2.IsSelected = item.IsMain.Value;
                Emails.Add(tmp2);
            }
        }

        #endregion InitValidation object data
    }
}