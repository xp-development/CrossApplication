using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CrossApplication.Core.Common;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Net.Contracts.Navigation;
using CrossApplication.Wpf.Application.Shell.RibbonTabs;
using CrossApplication.Wpf.Common;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Common.ViewModels;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace CrossApplication.Wpf.Application.Shell
{
    public class RichShellViewModel : IViewLoadedAsync
    {
        private readonly IEnumerable<IMainNavigationItem> _mainNavigationItems;
        private readonly INavigationService _navigationService;
        private readonly IEnumerable<IBackstageTabViewModel> _backstageTabs;
        private readonly IRegionManager _regionManager;
        public ObservableCollection<NavigationItem> NavigationItems { get; set; } = new ObservableCollection<NavigationItem>();

        public DelegateCommand ManageAccountsCommand { get; }
        public InteractionRequest<INotification> NotificationRequest { get; }

        public RichShellViewModel(InteractionRequest<INotification> notificationRequest, IEnumerable<IMainNavigationItem> mainNavigationItems, INavigationService navigationService, IEnumerable<IBackstageTabViewModel> backstageTabs, IRegionManager regionManager)
        {
            _mainNavigationItems = mainNavigationItems;
            _navigationService = navigationService;
            _backstageTabs = backstageTabs;
            _regionManager = regionManager;
            NotificationRequest = notificationRequest;

            ManageAccountsCommand = new DelegateCommand(() => NotificationRequest.Raise(new Notification {Title = LocalizationManager.Get(typeof(Common.Properties.Resources), "AccountManagementTitle")}));
        }

        public Task OnViewLoadedAsync()
        {
            foreach (var mainNavigationItem in _mainNavigationItems)
            {
                NavigationItems.Add(new NavigationItem(_navigationService, mainNavigationItem.Label, mainNavigationItem.NavigationKey));
            }

            foreach (var backstageTab in _backstageTabs.OrderBy(x => x.Position))
            {
                _regionManager.AddToRegion(RegionNames.BackstageRegion, backstageTab);
            }

            return Task.FromResult(false);
        }
    }
}