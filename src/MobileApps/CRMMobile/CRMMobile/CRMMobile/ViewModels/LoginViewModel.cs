using CRMMobile.Helper;
using CRMMobile.Validations;
using IO.Swagger.Api;
using IO.Swagger.Client;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRMMobile.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(INavigationService navigationService, ITokenApi tokenApi)
            : base(navigationService)
        {
            TokenApi = tokenApi;
            InitValidation();
        }

        public DelegateCommand LoginCommand => new DelegateCommand(async () => await Login());

        public ITokenApi TokenApi { get; set; }

        private ValidationObject<string> username;

        public ValidationObject<string> Username
        {
            get
            {
                return username;
            }
            set
            {
                SetProperty(ref username, value);
            }
        }

        private ValidationObject<string> password;

        public ValidationObject<string> Password
        {
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password, value);
            }
        }

        private bool isEnable;

        public bool IsEnable
        {
            get => isEnable;
            set { SetProperty(ref isEnable, value); }
        }

        private bool isRemember;

        public bool IsRemember
        {
            get => isRemember;
            set { SetProperty(ref isRemember, value); }
        }

        private void InitValidation()
        {
            Username = new ValidationObject<string>();
            Password = new ValidationObject<string>();
            Username.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกชื่อผู้ใช้" });
            Password.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณากรอกรหัสผ่าน" });
        }

        private bool Validate()
        {
            bool isValidUser = username.Validate();
            bool isValidPassword = password.Validate();
            return isValidUser && isValidPassword;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            //Username.Value = "TEST_LC01";
            //Password.Value = "0000";
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (UserIdentify.IsRemember)
            {
                Username.Value = UserIdentify.Username;
                Password.Value = UserIdentify.Password;
                IsRemember = UserIdentify.IsRemember;
            }

            //await AutoLogin();
        }

        private async Task Login()
        {
            if (!Validate())
                return;

            try
            {
                IsEnable = false;
                IsBusy = true;
                if (IsRemember)
                {
                    UserIdentify.Username = Username.Value;
                    UserIdentify.Password = Password.Value;
                    UserIdentify.IsRemember = IsRemember;
                }

                await Run(() => TokenApi.Login(new IO.Swagger.Model.LoginParam() { Username = username.Value, GrantType = GrantType.Password }));
                NavigateToMainPage();
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; IsEnable = true; }
        }

        private async Task AutoLogin()
        {
            if (UserIdentify.IsRemember && UserIdentify.IsLoggedIn && UserIdentify.AccessToken != string.Empty)
            {
                try
                {
                    Username.Value = UserIdentify.Username;
                    Password.Value = UserIdentify.Password;
                    IsRemember = UserIdentify.IsRemember;

                    IsEnable = false;
                    IsBusy = true;

                    await Task.Run(() => TokenApi.Login(new IO.Swagger.Model.LoginParam()
                    {
                        RefreshToken = UserIdentify.RefreshToken,
                        GrantType = GrantType.RefreshToken
                    }));
                    Username.IsValid = Password.IsValid = true;
                    NavigateToMainPage();
                }
                catch (ApiException e)
                {
                    HandleException(e);
                }
                finally { IsBusy = false; IsEnable = true; }
            }
        }

        public void NavigateToMainPage()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await NavigationService.NavigateAsync("/Menu/Navigation/MyWorld");
                //await NavigationService.GoBackToRootAsync();
            });
        }
    }
}