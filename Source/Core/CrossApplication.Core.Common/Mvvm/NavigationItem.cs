using CrossApplication.Core.Contracts.Common.Navigation;
using Prism.Commands;
using Prism.Mvvm;

namespace CrossApplication.Core.Common.Mvvm
{
    public class NavigationItem : BindableBase
    {
        public string Label { get; }
        public DelegateCommand NavigateCommand { get; private set; }

        public NavigationItem(INavigationService navigationService, string label, string navigationKey)
        {
            _navigationService = navigationService;
            _navigationKey = navigationKey;

            Label = label;
            NavigateCommand = new DelegateCommand(OnNavigate, CanNavigate);
        }

        private async void OnNavigate()
        {
            _isNavigating = true;
            await _navigationService.NavigateToAsync(_navigationKey);
            _isNavigating = false;
        }

        private bool CanNavigate()
        {
            return !_isNavigating;
        }

        private readonly string _navigationKey;
        private readonly INavigationService _navigationService;
        private bool _isNavigating;
    }
}