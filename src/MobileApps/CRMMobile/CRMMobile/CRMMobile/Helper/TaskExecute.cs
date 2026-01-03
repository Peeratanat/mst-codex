using IO.Swagger.Client;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CRMMobile.Helper
{
    public static class TaskExecute
    {
        public async static Task<TResult> Run<TResult>(Func<TResult> function) where TResult : class
        {
            try
            {
                if (!App.IsConnection)
                    await App.Current.MainPage.DisplayAlert(" ", "กรุณาตรวจสอบการเชื่อมต่ออินเทอเน็ต", "ปิด", null);

                return await Task.Run(function);
            }
            catch (ApiException e)
            {
                var _content = e.ErrorContent as string;
                var isPopupErr = _content.Contains("PopupErrors");
                var isFieldErr = _content.Contains("FieldErrors");

                if (isPopupErr || isFieldErr)
                {
                    var errors = JsonConvert.DeserializeObject<ValidationException>(_content);
                    if (errors.PopupErrors != null && errors.PopupErrors.Count > 0)
                    {
                        var str = errors.PopupErrors.Select(t => t.Message).Aggregate((a, b) => a + ", " + b);
                        await App.Current.MainPage.DisplayAlert("แจ้งเตือน", str, "ปิด");
                    }

                    if (errors.FieldErrors != null && errors.FieldErrors.Count > 0)
                    {
                        var str = errors.FieldErrors.Select(t => t.Message).Aggregate((a, b) => a + ", " + b);
                        await App.Current.MainPage.DisplayAlert("แจ้งเตือน", str, "ปิด");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("แจ้งเตือน", e.Message.ToString(), "ปิด");
                }

                return await Task.FromResult<TResult>(null);
            }
        }

        public async static Task<T> Execute<T>(Task<T> task) where T : class
        {
            try
            {
                var result = await task;
                return result;
            }
            catch (ApiException e)
            {
                var _content = e.ErrorContent as string;
                var isPopupErr = _content.Contains("PopupErrors");
                var isFieldErr = _content.Contains("FieldErrors");

                if (isPopupErr || isFieldErr)
                {
                    var errors = JsonConvert.DeserializeObject<ValidationException>(_content);
                    if (errors.PopupErrors != null && errors.PopupErrors.Count > 0)
                    {
                        var str = errors.PopupErrors.Select(t => t.Message).Aggregate((a, b) => a + ", " + b);
                        await App.Current.MainPage.DisplayAlert("แจ้งเตือน", str, "ปิด");
                    }

                    if (errors.FieldErrors != null && errors.FieldErrors.Count > 0)
                    {
                        var str = errors.FieldErrors.Select(t => t.Message).Aggregate((a, b) => a + ", " + b);
                        await App.Current.MainPage.DisplayAlert("แจ้งเตือน", str, "ปิด");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("แจ้งเตือน", e.Message.ToString(), "ปิด");
                }

                return await Task.FromResult<T>(null);
            }
        }
    }

    public static class CallService
    {
        public async static Task<object> Test(Action action)
        {
            //var service = DependencyService.Get<ITokenApi>();
            //service.Login(new IO.Swagger.Model.LoginParam() { GrantType = "refreshtoken" });
            return await Task.Run(() => action);
        }
    }
}