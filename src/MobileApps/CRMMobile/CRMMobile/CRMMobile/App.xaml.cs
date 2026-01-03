using CRMMobile.Services;
using CRMMobile.ViewModels;
using CRMMobile.Views;
using CRMMobile.Views.Popup;
using IO.Swagger.Api;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity;
using Unity.Injection;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CRMMobile
{
    public partial class App
    {
        /*
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor.
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        public static bool IsConnection { get; set; }
        public static bool IsVPNConnected => App.VPNConnected();
        public static Dictionary<string, string> ServiceUrl { get; set; } = new Dictionary<string, string>();
        private static string CustomerUrl = "http://crmrevo-customer-api-crmrevo-test.devops-app.apthai.com";
        private static string MasterCenterUrl = "http://crmrevo-masterdata-api-crmrevo-test.devops-app.apthai.com";
        private static string ProjectUrl = "http://crmrevo-project-api-crmrevo-test.devops-app.apthai.com";
        private static string IndentifyUrl = "http://crmrevo-identity-api-crmrevo-test.devops-app.apthai.com";

        public App() : this(null)
        {
        }

        private async void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            await Clipboard.SetTextAsync(ex.Message);
            Crashes.TrackError(ex);
        }

        public App(IPlatformInitializer initializer)
            : base(initializer, true)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            CheckConnection();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            await NavigationService.NavigateAsync("/Login");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>("Navigation");
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>("Login");
            containerRegistry.RegisterForNavigation<MenuPage, MenuViewModel>("Menu");
            containerRegistry.RegisterForNavigation<ContactPage, ContactViewModel>("Contact");
            containerRegistry.RegisterForNavigation<ContactForm, ContactFormViewModel>("ContactForm");
            containerRegistry.RegisterForNavigation<ContactDetail, ContactDetailViewModel>("ContactDetail");
            containerRegistry.RegisterForNavigation<ContactAddressForm, ContactAddressFormViewModel>("ContactAddressFrom");
            containerRegistry.RegisterForNavigation<ContactAddressOtherForm, ContactAddressOtherFormViewModel>("ContactAddressOtherFrom");
            containerRegistry.RegisterForNavigation<LeadPage, LeadViewModel>("Lead");
            containerRegistry.RegisterForNavigation<LeadFormPage, LeadFormViewModel>("LeadForm");
            containerRegistry.RegisterForNavigation<LeadDetailPage, LeadDetailViewModel>("LeadDetail");
            containerRegistry.RegisterForNavigation<LeadActivityFormPage, LeadActivityFormViewModel>("LeadActivityForm");
            containerRegistry.RegisterForNavigation<OpportunityPage, OpportunityViewModel>("Opportunity");
            containerRegistry.RegisterForNavigation<OpportunityFormPage, OpportunityFormViewModel>("OpportunityForm");
            containerRegistry.RegisterForNavigation<OpportunityDetailPage, OpportunityDetailViewModel>("OpportunityDetail");
            containerRegistry.RegisterForNavigation<OpportunityRevisitFormPage, OpportunityRevisitFormViewModel>("OpportunityRevisitForm");
            containerRegistry.RegisterForNavigation<OpportunityActivityFormPage, OpportunityActivityFormViewModel>("OpportunityActivityForm");
            containerRegistry.RegisterForNavigation<VisitorPage, VisitorViewModel>("Visitor");
            containerRegistry.RegisterForNavigation<VisitorDetailPage, VisitorDetailViewModel>("VisitorDetail");
            containerRegistry.RegisterForNavigation<BookingPage, BookingViewModel>("Booking");
            containerRegistry.RegisterForNavigation<MyWorld, MyWorldViewModel>("MyWorld");

            // Popup
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<PopupAutoComplete>();
            containerRegistry.RegisterForNavigation<ContactAddressPopup>("ContactAddressPopup");
            containerRegistry.RegisterForNavigation<QualifyPopup>("QualifyPopup");
            containerRegistry.RegisterForNavigation<DisQualifyPopup, ViewModelBase>("DisQualifyPopup");
            containerRegistry.RegisterForNavigation<QualifyContactPopup, QualifyContactPopupViewModel>("QualifyContactPopup");
            containerRegistry.RegisterForNavigation<QualifyLeadToContact>("QualifyLeadToContact");
            containerRegistry.RegisterForNavigation<LeadDeletePopup, LeadDeletePopupViewModel>("LeadDeletePopup");
            IUnityContainer container = ((UnityContainerExtension)(containerRegistry)).Instance;

            //Register DialogService
            container.RegisterType<IDialogService, DialogService>();

            //BaseUrl
            //ServiceUrl.Add("CustomerUrl", "http://crmrevo-customer-api-crmrevo-test.devops-app.apthai.com");
            //ServiceUrl.Add("MasterCenterUrl", "http://crmrevo-masterdata-api-crmrevo-test.devops-app.apthai.com");
            //ServiceUrl.Add("ProjectUrl", "http://crmrevo-project-api-crmrevo-test.devops-app.apthai.com");
            //ServiceUrl.Add("IndentifyUrl", "http://crmrevo-identity-api-crmrevo-test.devops-app.apthai.com");

            //Customer service
            container.RegisterType<ILeadsApi, LeadsApi>(new InjectionConstructor(CustomerUrl));
            container.RegisterType<IVisitorsApi, VisitorsApi>(new InjectionConstructor(CustomerUrl));
            container.RegisterType<IContactsApi, ContactsApi>(new InjectionConstructor(CustomerUrl));
            container.RegisterType<IOpportunitiesApi, OpportunitiesApi>(new InjectionConstructor(CustomerUrl));
            container.RegisterType<IActivitiesApi, ActivitiesApi>(new InjectionConstructor(CustomerUrl));
            // Mastercenter service
            container.RegisterType<IMasterCentersApi, MasterCentersApi>(new InjectionConstructor(MasterCenterUrl));
            container.RegisterType<ICountriesApi, CountriesApi>(new InjectionConstructor(MasterCenterUrl));
            container.RegisterType<IProvincesApi, ProvincesApi>(new InjectionConstructor(MasterCenterUrl));
            container.RegisterType<IDistrictsApi, DistrictsApi>(new InjectionConstructor(MasterCenterUrl));
            container.RegisterType<ISubDistrictsApi, SubDistrictsApi>(new InjectionConstructor(MasterCenterUrl));

            // Proeject service
            container.RegisterType<IProjectsApi, ProjectsApi>(new InjectionConstructor(ProjectUrl));

            // Identity
            container.RegisterType<ITokenApi, TokenApi>(new InjectionConstructor(IndentifyUrl));
            container.RegisterType<IUsersApi, UsersApi>(new InjectionConstructor(IndentifyUrl));
        }

        private void CheckConnection()
        {
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
                IsConnection = true;
            else
                IsConnection = false;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
                IsConnection = true;
            else
                IsConnection = false;
        }

        public static bool VPNConnected()
        {
            Ping myPing = new Ping();
            var builder = new UriBuilder(ServiceUrl["VPNServer"]);
            PingReply pingReply = myPing.Send(builder.Host);
            if (pingReply != null)
                return true;
            else
                return false;
        }
    }
}