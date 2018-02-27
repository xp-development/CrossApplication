using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Events;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Contracts.Views;

namespace CrossApplication.Wpf.Application.Shell
{
    public class RichShellViewModel : ViewModelBase, IViewActivatingAsync, IViewDeactivatedAsync
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();
        public ObservableCollection<NavigationItem> BackstageNavigationItems { get; } = new ObservableCollection<NavigationItem>();

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

        public RichShellViewModel(IEnumerable<IMainNavigationItem> mainNavigationItems, INavigationService navigationService, IEnumerable<IInfrastructureNavigationItem> backstageNavigationItems, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            foreach (var mainNavigationItem in mainNavigationItems)
                NavigationItems.Add(new NavigationItem(navigationService, mainNavigationItem.Label, mainNavigationItem.NavigationKey, mainNavigationItem.Glyph));

            foreach (var backstageNavigationItem in backstageNavigationItems)
                BackstageNavigationItems.Add(new NavigationItem(navigationService, backstageNavigationItem.Label, backstageNavigationItem.NavigationKey, backstageNavigationItem.Glyph));
        }

        public Task OnViewActivatingAsync(NavigationParameters navigationParameters)
        {
            _stateMessages = new BlockingCollection<string>();
            _eventAggregator.GetEvent<StateMessageEventPayload>().Subscribe(OnStateMessageEvent);
            _eventAggregator.GetEvent<ProgressMessageEventPayload>().Subscribe(OnProgressMessageEvent);
            HandleStateMessages();
            return Task.FromResult(false);
        }

        public Task OnViewDeactivatedAsync()
        {
            _stateMessages.CompleteAdding();
            _eventAggregator.GetEvent<StateMessageEventPayload>().Unsubscribe(OnStateMessageEvent);
            _eventAggregator.GetEvent<ProgressMessageEventPayload>().Unsubscribe(OnProgressMessageEvent);

            return Task.FromResult(false);
        }

        private Task OnStateMessageEvent(StateMessageEventPayload args)
        {
            _stateMessages.Add(args.Message);
            return Task.CompletedTask;
        }

        private Task OnProgressMessageEvent(ProgressMessageEventPayload args)
        {
            Progress = args.Progress;
            return Task.CompletedTask;
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