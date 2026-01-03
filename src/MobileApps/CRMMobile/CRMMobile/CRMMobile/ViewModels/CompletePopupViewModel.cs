using Prism.Navigation;

namespace CRMMobile.ViewModels
{
    public class CompletePopupViewModel : ViewModelBase
    {
        public CompletePopupViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            NavigationService.ClearPopupStackAsync();
        }
    }
}