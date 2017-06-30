using CrossApplication.Core.Contracts.Common.Navigation;
using Prism.Commands;
using Prism.Mvvm;

namespace CrossApplication.Core.Common.Mvvm
{
    public class NavigationItem : BindableBase
    {
        public string Label { get; }
        public string Glyph { get; }
        public DelegateCommand NavigateCommand { get; }

        public NavigationItem(INavigationService navigationService, string label, string navigationKey, string glyph)
        {
            _navigationService = navigationService;
            _navigationKey = navigationKey;

            Label = label;
            Glyph = glyph;
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