using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Application.Authorization;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Wpf.Application.Properties;
using Prism.Mvvm;
using Prism.Navigation;
using INavigationService = CrossApplication.Core.Contracts.Common.Navigation.INavigationService;

namespace CrossApplication.Wpf.Application.Login
{
    public class LoginViewModel : BindableBase, IViewActivatingAsync, IViewActivatedAsync
    {
        public ObservableCollection<IAuthorizationProvider> AuthProviders { get; } = new ObservableCollection<IAuthorizationProvider>();

        public string Message
        {
            get => _message;
            private set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        public IAuthorizationProvider SelectedAuthProvider
        {
            get { return _selectedAuthProvider; }
            set
            {
                _selectedAuthProvider = value;
                LoginAsync();
            }
        }

        public LoginViewModel(IUserManager userManager, INavigationService navigationService, IEnumerable<IAuthorizationProvider> authorizationProviders)
        {
            _userManager = userManager;
            _navigationService = navigationService;

            foreach (var authorizationProvider in authorizationProviders)
                AuthProviders.Add(authorizationProvider);
        }

        public Task OnViewActivatingAsync(NavigationParameters navigationParameters)
        {
            _requestedView = (string) navigationParameters["RequestedView"];
            return Task.FromResult(false);
        }

        public Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            Message = "";
            return Task.FromResult(false);
        }

        private async void LoginAsync()
        {
            var isLoggedIn = await _userManager.LoginAsync(SelectedAuthProvider);
            if (isLoggedIn)
                await _navigationService.NavigateToAsync(_requestedView);
            else
                Message = Resources.LoginFailed;
        }

        private readonly INavigationService _navigationService;
        private readonly IUserManager _userManager;
        private string _message;
        private string _requestedView;
        private IAuthorizationProvider _selectedAuthProvider;
    }
}