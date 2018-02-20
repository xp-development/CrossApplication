using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Contracts.Views;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using INavigationService = CrossApplication.Core.Contracts.Common.Navigation.INavigationService;

namespace CrossApplication.Wpf.Application.Shell
{
    public class RichShellViewModel : ViewModelBase, IViewActivatingAsync, IViewDeactivatedAsync
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();
        public ObservableCollection<NavigationItem> BackstageNavigationItems { get; } = new ObservableCollection<NavigationItem>();
        public InteractionRequest<INotification> NotificationRequest { get; }

        public string StateMessage
        {
            get => _stateMessage;
            private set
            {
                _stateMessage = value;
                NotifyPropertyChanged();
            }
        }

        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                NotifyPropertyChanged();
            }
        }

        public RichShellViewModel(InteractionRequest<INotification> notificationRequest, IEnumerable<IMainNavigationItem> mainNavigationItems, INavigationService navigationService, IEnumerable<IInfrastructureNavigationItem> backstageNavigationItems, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            foreach (var mainNavigationItem in mainNavigationItems)
                NavigationItems.Add(new NavigationItem(navigationService, mainNavigationItem.Label, mainNavigationItem.NavigationKey, mainNavigationItem.Glyph));

            foreach (var backstageNavigationItem in backstageNavigationItems)
                BackstageNavigationItems.Add(new NavigationItem(navigationService, backstageNavigationItem.Label, backstageNavigationItem.NavigationKey, backstageNavigationItem.Glyph));

            NotificationRequest = notificationRequest;
        }

        public Task OnViewActivatingAsync(NavigationParameters navigationParameters)
        {
            _stateMessages = new BlockingCollection<string>();
            _eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Subscribe(OnStateMessageEvent, ThreadOption.BackgroundThread);
            _eventAggregator.GetEvent<PubSubEvent<ProgressMessageEvent>>().Subscribe(OnProgressMessageEvent, ThreadOption.BackgroundThread);
            HandleStateMessages();
            return Task.FromResult(false);
        }

        public Task OnViewDeactivatedAsync()
        {
            _stateMessages.CompleteAdding();
            _eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Unsubscribe(OnStateMessageEvent);
            _eventAggregator.GetEvent<PubSubEvent<ProgressMessageEvent>>().Unsubscribe(OnProgressMessageEvent);

            return Task.FromResult(false);
        }

        private void OnStateMessageEvent(StateMessageEvent args)
        {
            _stateMessages.Add(args.Message);
        }

        private void OnProgressMessageEvent(ProgressMessageEvent args)
        {
            Progress = args.Progress;
        }

        private void HandleStateMessages()
        {
            Task.Run(() =>
            {
                while (!_stateMessages.IsCompleted)
                {
                    if(_stateMessages.TryTake(out var message))
                        StateMessage = message;
                    Thread.Sleep(TimeSpan.FromMilliseconds(800));
                }
            });
        }

        private readonly IEventAggregator _eventAggregator;
        private BlockingCollection<string> _stateMessages;
        private int _progress;
        private string _stateMessage;
    }
}