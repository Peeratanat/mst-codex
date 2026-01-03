using CRMMobile.Models;
using IO.Swagger.Api;
using IO.Swagger.Client;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRMMobile.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NavigateCommand = new DelegateCommand(Navigate);
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            LogoutCommand = new DelegateCommand(async () => Logout());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
            Init();
        }

        [Unity.Dependency]
        public ITokenApi TokenApi { get; set; }

        //[Unity.Dependency]
        //public IUsersApi UsersApi { get; set; }

        private void Init()
        {
            MenuItems = new ObservableCollection<MainMenuItem>();
            MenuItems.Add(new MainMenuItem()
            {
                Icon = "\ue925",
                PageName = "MyWorld",
                Title = "My World",
                SelectedColorItem = Color.Red
            });

            MenuItems.Add(new MainMenuItem()
            {
                Icon = "\ue946",
                PageName = "Visitor",
                Title = "Visitor",
            });

            MenuItems.Add(new MainMenuItem()
            {
                Icon = "\uE803",
                PageName = "Lead",
                Title = "Lead",
            });

            MenuItems.Add(new MainMenuItem()
            {
                Icon = "\uE801",
                PageName = "Contact",
                Title = "Contact",
            });

            MenuItems.Add(new MainMenuItem()
            {
                Icon = "\ue913",
                PageName = "Opportunity",
                Title = "Opportunity",
            });

            RaisePropertyChanged("MenuItems");
        }

        //private string fullName;
        //public string FullName
        //{
        //    get => fullName;
        //    set { SetProperty(ref fullName, value); }
        //}

        public ObservableCollection<MainMenuItem> MenuItems { get; private set; }
        public MainMenuItem SelectedMenuItem { get; set; }
        public DelegateCommand NavigateCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        private async void Navigate()
        {
            await NavigationService.NavigateAsync("Navigation/" + SelectedMenuItem.PageName);
        }

        public async Task Logout()
        {
            await RunWithoutReturn(() => TokenApi.Logout(new IO.Swagger.Model.LogoutParam() { RefreshToken = UserIdentify.RefreshToken }));
            await Navigate("/Login");
            await NavigationService.GoBackToRootAsync();
        }
    }
}