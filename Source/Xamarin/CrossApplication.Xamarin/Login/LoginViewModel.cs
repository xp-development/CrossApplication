using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Application.Authorization;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;

namespace CrossApplication.Xamarin.Login
{
    public class LoginViewModel : ViewModelBase, IViewActivatingAsync, IViewActivatedAsync
    {
        public ObservableCollection<IAuthorizationProvider> AuthProviders { get; } = new ObservableCollection<IAuthorizationProvider>();

        public string Message
        {
            get => _message;
            private set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }

        public IAuthorizationProvider SelectedAuthProvider
        {
            get => _selectedAuthProvider;
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

        public Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            Message = "";
            return Task.FromResult(false);
        }

        public Task OnViewActivatingAsync(NavigationParameters navigationParameters)
        {
            _requestedView = (string) navigationParameters["RequestedView"];
            return Task.FromResult(false);
        }

        private async void LoginAsync()
        {
            var isLoggedIn = await _userManager.LoginAsync(SelectedAuthProvider);
            if (isLoggedIn)
                await _navigationService.NavigateToAsync(_requestedView);
            else
                Message = "Login failed!";
        }

        private readonly INavigationService _navigationService;
        private readonly IUserManager _userManager;
        private string _message;
        private string _requestedView;
        private IAuthorizationProvider _selectedAuthProvider;
    }
}