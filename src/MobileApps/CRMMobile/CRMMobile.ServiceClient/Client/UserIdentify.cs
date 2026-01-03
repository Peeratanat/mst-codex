using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
namespace IO.Swagger.Client
{
    public class UserIdentify
    {
        private static UserIdentify _instance = new UserIdentify();
        private UserIdentify() { }
        static internal UserIdentify Instance()
        {
            return _instance;
        }

        private static ISettings AppSettings => CrossSettings.Current;

        public static bool IsRemember
        {
            get => AppSettings.GetValueOrDefault(nameof(IsRemember), false);
            set => AppSettings.AddOrUpdateValue(nameof(IsRemember), value);
        }

        public static string Username
        {
            get => AppSettings.GetValueOrDefault(nameof(Username), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Username), value);
        }

        public static string Password
        {
            get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Password), value);
        }

        public static string AccessToken
        {
            get => AppSettings.GetValueOrDefault(nameof(AccessToken), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(AccessToken), value);
        }

        public static string RefreshToken
        {
            get => AppSettings.GetValueOrDefault(nameof(RefreshToken), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(RefreshToken), value);
        }

       
        public static long? Expires
        {
            get
            {
                var _expires = AppSettings.GetValueOrDefault(nameof(Expires), string.Empty);
                long i;
                if (long.TryParse(_expires, out i))
                    return i;
                return null;
            }
            set => AppSettings.AddOrUpdateValue(nameof(Expires), value.ToString());
        }

        public static long? ExpiresIn
        {
            get
            {
                var _expiresIn = AppSettings.GetValueOrDefault(nameof(ExpiresIn), string.Empty);
                long i;
                if (long.TryParse(_expiresIn, out i)) return i;
                return null;
            }
            set => AppSettings.AddOrUpdateValue(nameof(ExpiresIn), value.ToString());
        }


        public static Guid? UserId
        {
            get
            {
                var _userId = AppSettings.GetValueOrDefault(nameof(UserId), string.Empty);
                Guid i;
                if (Guid.TryParse(_userId, out i))
                    return i;
                return null;
            }
            set { AppSettings.AddOrUpdateValue(nameof(UserId), value.ToString()); }
        }

        public static string InstallId
        {
            get => AppSettings.GetValueOrDefault(nameof(InstallId), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(InstallId), value);
        }

        public static bool IsLoggedIn { get { return !string.IsNullOrEmpty(AccessToken) ? true : false; } }
        public static bool IsExpired
        {
            get
            {
                if (Expires == null)
                    return false;

                var _expireDate = DateTimeOffset.FromUnixTimeSeconds(Expires.Value).DateTime;
                //var _expires = DateTime.Now.AddSeconds((double)ExpiresIn);
                if (DateTime.Compare(DateTime.Now, _expireDate) == 2)
                    return true;
                else
                    return false;
            }
        }

        public static string Displayname
        {
            get => AppSettings.GetValueOrDefault(nameof(Displayname), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(Displayname), value);
        }

        public static void Remove(string propertyName)
        {
            AppSettings.Remove(propertyName);
        }

        public static void ClearAll()
        {
            AppSettings.Clear();
        }

        public static void ClearToken()
        {
       
        }

        public static bool IsReLogin()
        {
            return UserIdentify.IsLoggedIn && UserIdentify.IsExpired;
        }
    }
}
