using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Common.Navigation;

namespace CrossApplication.Core.Common.Mvvm
{
    public class NavigationItem : ViewModelBase
    {
        public string Label { get; }
        public string Glyph { get; }
        public DelegateCommand<object, object> NavigateCommand { get; }

        public NavigationItem(INavigationService navigationService, string label, string navigationKey, string glyph)
        {
            _navigationService = navigationService;
            _navigationKey = navigationKey;

            Label = label;
            Glyph = glyph;
            NavigateCommand = new DelegateCommand<object, object>(OnNavigate, CanNavigate);
        }

        private async Task OnNavigate(object args)
        {
            _isNavigating = true;
            await _navigationService.NavigateToAsync(_navigationKey);
            _isNavigating = false;
        }

        private bool CanNavigate(object args)
        {
            return !_isNavigating;
        }

        private readonly string _navigationKey;
        private readonly INavigationService _navigationService;
        private bool _isNavigating;
    }
}