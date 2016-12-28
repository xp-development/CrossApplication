using System.Threading.Tasks;
using System.Windows.Input;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Wpf.Application.Properties;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace CrossApplication.Wpf.Application.Login
{
    public class LoginViewModel : BindableBase, INavigationAware, IViewLoadedAsync
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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _requestedView = (string) navigationContext.Parameters["RequestedView"];
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public Task OnViewLoadedAsync()
        {
            Message = "";
            return Task.FromResult(false);
        }

        private async Task LoginAsync()
        {
            var isLoggedIn = await _userManager.LoginAsync(UserName);
            if (isLoggedIn)
                _navigationService.NavigateTo(_requestedView);
            else
                Message = Resources.LoginFailed;
        }

        private readonly INavigationService _navigationService;
        private readonly IUserManager _userManager;
        private string _message;
        private string _requestedView;
    }
}