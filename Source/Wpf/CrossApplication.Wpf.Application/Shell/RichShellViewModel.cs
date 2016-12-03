using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Net.Contracts.Navigation;
using CrossApplication.Wpf.Application.Shell.RibbonTabs;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Common.ViewModels;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;

namespace CrossApplication.Wpf.Application.Shell
{
    public class RichShellViewModel : BindableBase, IViewLoadedAsync, IViewUnloadedAsync
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; set; } = new ObservableCollection<NavigationItem>();

        public InteractionRequest<INotification> NotificationRequest { get; }

        public string StateMessage
        {
            get { return _stateMessage; }
            private set
            {
                _stateMessage = value;
                OnPropertyChanged();
            }
        }

        public RichShellViewModel(InteractionRequest<INotification> notificationRequest, IEnumerable<IMainNavigationItem> mainNavigationItems, INavigationService navigationService, IEnumerable<IBackstageTabViewModel> backstageTabs,
            IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _mainNavigationItems = mainNavigationItems;
            _navigationService = navigationService;
            _backstageTabs = backstageTabs;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            NotificationRequest = notificationRequest;
       }

        private void OnStateMessageEvent(StateMessageEvent args)
        {
            StateMessage = args.Message;
        }

        public Task OnViewLoadedAsync()
        {
            _eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Subscribe(OnStateMessageEvent);

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

        public Task OnViewUnloadedAsync()
        {
            _eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Unsubscribe(OnStateMessageEvent);

            return Task.FromResult(false);
        }

        private readonly IEnumerable<IBackstageTabViewModel> _backstageTabs;
        private readonly IEnumerable<IMainNavigationItem> _mainNavigationItems;
        private readonly INavigationService _navigationService;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private string _stateMessage;
    }
}