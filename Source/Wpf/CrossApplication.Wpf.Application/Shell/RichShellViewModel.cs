using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Core.Wpf.Contracts.Backstages;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Navigation;
using INavigationService = CrossApplication.Core.Contracts.Common.Navigation.INavigationService;

namespace CrossApplication.Wpf.Application.Shell
{
    public class RichShellViewModel : BindableBase, IViewActivatingAsync, IViewActivatedAsync, IViewDeactivatedAsync
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();
        public ObservableCollection<NavigationItem> BackstageNavigationItems { get; } = new ObservableCollection<NavigationItem>();

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

        public RichShellViewModel(InteractionRequest<INotification> notificationRequest, IEnumerable<IMainNavigationItem> mainNavigationItems, INavigationService navigationService, IEnumerable<IBackstageNavigationItem> backstageNavigationItems, IEventAggregator eventAggregator)
        {
            _mainNavigationItems = mainNavigationItems;
            _navigationService = navigationService;
            _backstageNavigationItems = backstageNavigationItems;
            _eventAggregator = eventAggregator;

            NotificationRequest = notificationRequest;
       }

        private void OnStateMessageEvent(StateMessageEvent args)
        {
            _stateMessages.Add(args.Message);
        }

        public Task OnViewActivatingAsync(NavigationParameters navigationParameters)
        {
            _eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Subscribe(OnStateMessageEvent, ThreadOption.BackgroundThread);
            HandleStateMessages();
            return Task.FromResult(false);
        }

        public Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            foreach (var mainNavigationItem in _mainNavigationItems)
            {
                NavigationItems.Add(new NavigationItem(_navigationService, mainNavigationItem.Label, mainNavigationItem.NavigationKey));
            }

            foreach (var backstageNavigationItem in _backstageNavigationItems)
            {
                BackstageNavigationItems.Add(new NavigationItem(_navigationService, backstageNavigationItem.Label, backstageNavigationItem.NavigationKey));
            }

            return Task.FromResult(false);
        }

        public Task OnViewDeactivatedAsync()
        {
            _stateMessages.CompleteAdding();
            _eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Unsubscribe(OnStateMessageEvent);

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

        private readonly IEnumerable<IBackstageNavigationItem> _backstageNavigationItems;
        private readonly IEnumerable<IMainNavigationItem> _mainNavigationItems;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private string _stateMessage;
        private readonly BlockingCollection<string> _stateMessages = new BlockingCollection<string>();
    }
}