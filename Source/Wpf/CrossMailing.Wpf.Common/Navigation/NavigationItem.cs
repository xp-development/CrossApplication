using System;
using CrossMailing.Common;
using CrossMailing.Wpf.Common.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace CrossMailing.Wpf.Common.Navigation
{
    public class NavigationItem : BindableBase
    {
        public string Label
        {
            get { return _label; }
            private set
            {
                _label = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand NavigateCommand { get; private set; }

        public NavigationItem(IEventAggregator eventAggregator, Guid moduleIdentifier, ResourceValue resourceValue)
        {
            _eventAggregator = eventAggregator;
            _moduleIdentifier = moduleIdentifier;
            Label = LocalizationManager.Get(resourceValue);
            NavigateCommand = new DelegateCommand(OnNavigate);
        }

        private void OnNavigate()
        {
            _eventAggregator.GetEvent<ActivateModuleEvent>().Publish(new ActivateModulePayload(_moduleIdentifier));
        }

        private readonly IEventAggregator _eventAggregator;
        private readonly Guid _moduleIdentifier;
        private string _label;
    }
}