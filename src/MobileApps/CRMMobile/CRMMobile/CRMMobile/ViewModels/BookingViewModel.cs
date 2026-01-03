using Prism.Commands;
using Prism.Navigation;

namespace CRMMobile.ViewModels
{
    public class BookingViewModel : ViewModelBase
    {
        public BookingViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        public DelegateCommand DisplaySearchcommand => new DelegateCommand(() =>
        {
            if (IsDisplaySearch)
            {
                IsDisplaySearch = false;
            }
            else
            {
                IsDisplaySearch = true;
            }
        });

        private bool isDisplaySearch;

        public bool IsDisplaySearch
        {
            get => isDisplaySearch;
            set
            {
                SetProperty(ref isDisplaySearch, value);
                RaisePropertyChanged(nameof(IsNotDisplaySearch));
            }
        }

        public bool IsNotDisplaySearch { get => !IsDisplaySearch; }
    }
}