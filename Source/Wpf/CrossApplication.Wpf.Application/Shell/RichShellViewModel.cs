using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Wpf.Application.Shell.RibbonTabs;
using CrossApplication.Wpf.Common.Navigation;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using IRegionManager = Prism.Regions.IRegionManager;

namespace CrossApplication.Wpf.Application.Shell
{
    public class RichShellViewModel : BindableBase, IViewLoadedAsync, IViewUnloadedAsync
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();

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
            _stateMessages.Add(args.Message);
        }

        public Task OnViewLoadedAsync()
        {
            _eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Subscribe(OnStateMessageEvent, ThreadOption.BackgroundThread);

            foreach (var mainNavigationItem in _mainNavigationItems)
            {
                NavigationItems.Add(new NavigationItem(_navigationService, mainNavigationItem.Label, mainNavigationItem.NavigationKey));
            }

            foreach (var backstageTab in _backstageTabs.OrderBy(x => x.Position))
            {
                _regionManager.AddToRegion(RegionNames.BackstageRegion, backstageTab);
            }

            HandleStateMessages();

            return Task.FromResult(false);
        }

        private void HandleStateMessages()
        {
            Task.Run(() =>
            {
                while (!_stateMessages.IsCompleted)
                {
                    StateMessage = _stateMessages.Take();
                    Thread.Sleep(TimeSpan.FromMilliseconds(800));
                }
            });
        }

        public Task OnViewUnloadedAsync()
        {
            _stateMessages.CompleteAdding();
            _eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Unsubscribe(OnStateMessageEvent);

            return Task.FromResult(false);
        }

        private readonly IEnumerable<IBackstageTabViewModel> _backstageTabs;
        private readonly IEnumerable<IMainNavigationItem> _mainNavigationItems;
        private readonly INavigationService _navigationService;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private string _stateMessage;
        private readonly BlockingCollection<string> _stateMessages = new BlockingCollection<string>();
    }
}