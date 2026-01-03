using System;
using System.Globalization;
using System.Threading;
using CarouselView.FormsPlugin.iOS;
using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Prism;
using Prism.Ioc;
using UIKit;
using Xamarin.Forms;

namespace CRMMobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            // UINavigationBar
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });
           
            InitThaiCulture();

            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            AiForms.Layouts.LayoutsInit.Init();
            CarouselViewRenderer.Init();
            LoadApplication(new App(new iOSInitializer()));
            AppCenter.Start("4ac9ec79-2456-4d05-836d-21bb350a04fa", typeof(Push), typeof(Crashes), typeof(Analytics));
            return base.FinishedLaunching(app, options);
        }

        private async void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Crashes.TrackError(ex);
        }

        private void InitThaiCulture()
        {
            var localeIdentifier = NSLocale.CurrentLocale.LocaleIdentifier;
            if (localeIdentifier == "th_TH")
            {
                new System.Globalization.ThaiBuddhistCalendar();
                CultureInfo newCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentUICulture = newCulture;
                // Make current UI culture consistent with current culture.
                Thread.CurrentThread.CurrentCulture = newCulture;
                //new System.Globalization.calendar
                //new System.Globalization.ThaiBuddhistCalendar();
            }
        }

    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
