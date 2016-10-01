using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Application.Navigation;
using Prism.Commands;
using Prism.Mvvm;

namespace CrossApplication.Wpf.Common.Navigation
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
            NavigateCommand = new DelegateCommand(OnNavigate);
        }

        private void OnNavigate()
        {
            _navigationService.NavigateTo(_navigationKey);
        }

        private readonly string _navigationKey;
        private readonly INavigationService _navigationService;
    }
}