using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Mail.Contracts.Messaging;
using INavigationService = CrossApplication.Core.Contracts.Common.Navigation.INavigationService;

namespace CrossApplication.Mail.Wpf.Navigation
{
    public class NavigationViewModel : IViewActivatedAsync, IViewDeactivatedAsync
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();

        public NavigationViewModel(INavigationService navigationService, IMailManager mailManager)
        {
            _navigationService = navigationService;
            _mailManager = mailManager;
        }

        public async Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            foreach (var mailAccount in await _mailManager.GetAccountsAsync())
            {
                foreach (var mailFolder in mailAccount.Folders)
                {
                    NavigationItems.Add(new NavigationItem(_navigationService, mailFolder.Name, "", ""));
                }
            }
        }

        public Task OnViewDeactivatedAsync()
        {
            NavigationItems.Clear();
            return Task.FromResult(false);
        }

        private readonly INavigationService _navigationService;
        private readonly IMailManager _mailManager;
    }
}