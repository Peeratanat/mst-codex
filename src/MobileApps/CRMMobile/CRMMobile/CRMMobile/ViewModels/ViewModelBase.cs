using CRMMobile.Helper;
using CRMMobile.Services;
using IO.Swagger.Api;
using IO.Swagger.Client;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Pages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRMMobile.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        [Unity.Dependency]
        public IDialogService DialogService { get; set; }

        protected INavigationService NavigationService { get; private set; }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _IsBusy;

        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetProperty(ref _IsBusy, value); }
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public ViewModelBase(INavigationService navigationService)
        {
            PageSize = 10;
            PageIndex = 1;
            NavigationService = navigationService;
        }

        #region command

        public DelegateCommand NavigateBackCommand => new DelegateCommand(async () =>
        {
            await NavigatedBack();
        });

        public DelegateCommand<string> NavigatePopupWithClear => new DelegateCommand<string>(async (value) =>
        {
            await NavigationService.ClearPopupStackAsync();
            await NavigationService.NavigateAsync(value);
        });

        public DelegateCommand SaveCommad { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion command

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                OnNavigatedModeNew(parameters);
            }
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedModeNew(INavigationParameters parameters)
        {
        }

        public virtual void Destroy()
        {
        }

        public async Task DisplaySuccessPopup(Action callBackAction = null)
        {
            await DialogService.DisplayCompletePopup(action: callBackAction);
        }

        public async Task ConfirmPopup(string title, string message, string acceptText, string cancelText, Action onSayYes = null, Action onSayNo = null)
        {
            await DialogService.ConfirmAlertAsync(title, message, acceptText, cancelText, onSayYes, onSayNo);
        }

        public async Task DisplayAlert(string title, string message, string cancelText, Action cancelCallback = null)
        {
            await DialogService.DisplayAlertAsync(title, message, cancelCallback);
        }

        public async Task DisplayCustomPopup(PopupPage page)
        {
            await DialogService.DisplayPopup(page);
        }

        public async Task NavigatedBack(INavigationParameters param = null)
        {
            await NavigationService.GoBackAsync(param);
        }

        public async Task Navigate(string page, NavigationParameters param = null, Action onSuccess = null)
        {
            await NavigationService.NavigateAsync(page, param);
            onSuccess?.Invoke();
        }

        public async Task<bool> CheckConnection(Action failedCollback = null, bool isFaultConnectionDisplayAlert = false)
        {
            if (App.IsConnection)
            {
                return true;
            }
            else
            {
                if (isFaultConnectionDisplayAlert)
                    await DisplayAlert(" ", "กรุณาตรวจสอบการเชื่อมต่ออินเทอเน็ต หรือการเชื่อมต่อ VPN", "ปิด", null);

                return false;
            }
        }

        public async void HandleException(Exception e)
        {
            IsBusy = false;
            if (e is ApiException)
            {
                var er = e as ApiException;
                if (er.ErrorContent == null)
                {
                    Crashes.TrackError(e);
                    return;
                }

                var _content = er.ErrorContent as string;
                var isPopupErr = _content.Contains("PopupErrors");
                var isFieldErr = _content.Contains("FieldErrors");

                if (isPopupErr || isFieldErr)
                {
                    var errors = JsonConvert.DeserializeObject<ValidationException>(_content);
                    if (errors.PopupErrors != null && errors.PopupErrors.Count > 0)
                    {
                        var str = errors.PopupErrors.Select(t => t.Message).Aggregate((a, b) => a + Environment.NewLine + b);
                        await DisplayAlert("แจ้งเตือน", str, "ปิด");
                    }

                    if (errors.FieldErrors != null && errors.FieldErrors.Count > 0)
                    {
                        var str = errors.FieldErrors.Select(t => t.Message).Aggregate((a, b) => a + Environment.NewLine + b);
                        await DisplayAlert("แจ้งเตือน", str, "ปิด");
                    }
                }
                else if (er.ErrorCode == 401)
                {
                    await DisplayAlert("แจ้งเตือน", "Unauthorized", "ปิด");
                }
                else
                {
                    await DisplayAlert("", er.ErrorContent.ToString(), " ปิด");
                    Crashes.TrackError(er);
                }
            }
            else if (e is ConnectionFailedException)
            {
                var ex = (ConnectionFailedException)e;
                if (!ex.IsInternetConnect)
                    await DisplayAlert("", ex.ConnectionMessage, "ปิด");
                if (!ex.IsVPNConnect)
                    await DisplayAlert("", ex.ConnectionMessage, "ปิด");
            }
            else
            {
                Crashes.TrackError(e);
            }
        }

        public async Task<T> Run<T>(Func<T> action)
        {
            if (!App.IsConnection)
                throw new ConnectionFailedException() { ConnectionMessage = "อุปกรณ์ไม่ได้เชื่อมต่ออินเทอร์เน็ต", IsInternetConnect = App.IsConnection };

            if (UserIdentify.IsReLogin())
                await LoginWithRefreshToken();

            return await Task.Run(action);
        }

        public async Task RunWithoutReturn(Action action)
        {
            if (UserIdentify.IsReLogin())
                await LoginWithRefreshToken();
            await Task.Run(action);
        }

        public async Task LoginWithRefreshToken()
        {
            var tokenApi = Xamarin.Forms.DependencyService.Get<ITokenApi>();
            await Task.Run(() => tokenApi.Login(new IO.Swagger.Model.LoginParam()
            {
                RefreshToken = UserIdentify.RefreshToken,
                GrantType = GrantType.RefreshToken
            }));
        }
    }
}