using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Mail.Contracts.Messaging;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Common.ViewModels;
using MailMessaging.Plain.Contracts;

namespace CrossApplication.Mail.Wpf.Navigation
{
    public class NavigationViewModel : IViewLoadedAsync
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();

        public NavigationViewModel(INavigationService navigationService, IMailMessenger messenger, IMailManager mailManager)
        {
            _navigationService = navigationService;
            _messenger = messenger;
            _mailManager = mailManager;
        }

        public async Task OnViewLoadedAsync()
        {
            foreach (var mailAccount in await _mailManager.GetAccountsAsync())
            {
                foreach (var mailFolder in mailAccount.Folders)
                {
                    NavigationItems.Add(new NavigationItem(_navigationService, mailFolder.Name, ""));
                }
            }
        }

        private readonly INavigationService _navigationService;
        private readonly IMailMessenger _messenger;
        private readonly IMailManager _mailManager;
    }
}