using System.Collections.Generic;
using System.Collections.ObjectModel;
using CrossApplication.Core.Common;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Net.Contracts.Navigation;
using CrossApplication.Wpf.Common.Navigation;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;

namespace CrossApplication.Wpf.Application.Shell
{
    public class RichShellViewModel
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public DelegateCommand ManageAccountsCommand { get; }
        public InteractionRequest<INotification> NotificationRequest { get; }


        public RichShellViewModel(InteractionRequest<INotification> notificationRequest, IEnumerable<IMainNavigationItem> mainNavigationItems, INavigationService navigationService)
        {
            NotificationRequest = notificationRequest;

            NavigationItems = new ObservableCollection<NavigationItem>();
            foreach (var mainNavigationItem in mainNavigationItems)
            {
                NavigationItems.Add(new NavigationItem(navigationService, mainNavigationItem.Label, mainNavigationItem.NavigationKey));
            }

            ManageAccountsCommand = new DelegateCommand(() => NotificationRequest.Raise(new Notification {Title = LocalizationManager.Get(typeof(Common.Properties.Resources), "AccountManagementTitle")}));
        }
    }
}