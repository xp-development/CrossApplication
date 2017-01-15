using System.Threading.Tasks;
using System.Windows.Input;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Wpf.Application.Properties;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using INavigationService = CrossApplication.Core.Contracts.Common.Navigation.INavigationService;

namespace CrossApplication.Wpf.Application.Login
{
    public class LoginViewModel : BindableBase, IViewActivatingAsync, IViewActivatedAsync
    {
        public string UserName { get; set; }
        public ICommand LoginCommand { get; }

        public string Message
        {
            get { return _message; }
            private set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel(IUserManager userManager, INavigationService navigationService)
        {
            _userManager = userManager;
            _navigationService = navigationService;
            LoginCommand = DelegateCommand.FromAsyncHandler(LoginAsync);
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

        private async Task LoginAsync()
        {
            var isLoggedIn = await _userManager.LoginAsync(UserName);
            if (isLoggedIn)
                await _navigationService.NavigateToAsync(_requestedView);
            else
                Message = Resources.LoginFailed;
        }

        private readonly INavigationService _navigationService;
        private readonly IUserManager _userManager;
        private string _message;
        private string _requestedView;
    }
}