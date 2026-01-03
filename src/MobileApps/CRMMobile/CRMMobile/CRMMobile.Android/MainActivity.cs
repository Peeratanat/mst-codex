using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using CarouselView.FormsPlugin.Android;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using System.Globalization;
using System.Threading;

namespace CRMMobile.Droid
{
    [Activity(Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            InitThaiCulture();
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            CarouselViewRenderer.Init();
            JoanZapata.XamarinIconify.Iconify
                .With(new JoanZapata.XamarinIconify.Fonts.FontAwesomeModule())
                .With(new JoanZapata.XamarinIconify.Fonts.IonIconsModule())
                .With(new FontIconsModule());

            var userSelectedCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = userSelectedCulture;
            var app = new App(new AndroidInitializer());

#if GORILLA
            // gorillaConfig
            var config = UXDivers.Gorilla.Droid.Player.UseApplication(app, this,
                new UXDivers.Gorilla.Config("Good Gorilla")
                .RegisterAssembly(typeof(CRMMobile.App).Assembly)
                .RegisterAssembliesFromTypes<
                    CRMMobile.Control.BorderEntry,
                    CRMMobile.Control.RadioEntry,
                    CRMMobile.Control.PickerEntry,
                    CRMMobile.Control.SFButton,
                    Prism.IActiveAware,
                    Prism.PrismApplicationBase,
                    Prism.Unity.PrismApplication>()
                    );
            LoadApplication(config);
#else
            LoadApplication(app);
#endif
            AppCenter.Start("23137f6a-4fc8-4107-8678-142030801395", typeof(Push), typeof(Crashes), typeof(Analytics));
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void InitThaiCulture()
        {
            var localeIdentifier = Java.Util.Locale.Default.ToString();
            if (localeIdentifier == "th_TH")
            {
                new System.Globalization.ThaiBuddhistCalendar();
                CultureInfo newCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentUICulture = newCulture;
                Thread.CurrentThread.CurrentCulture = newCulture;
            }
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

