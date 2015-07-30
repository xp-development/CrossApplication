using System.Collections.ObjectModel;
using CrossMailing.Wpf.Common.Events;
using CrossMailing.Wpf.Common.Navigation;
using Prism.Events;

namespace CrossMailing.Wpf.Application.Shell
{
    public class RichShellViewModel
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public RichShellViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<InitializeModuleEvent>().Subscribe(OnInizializeModule);

            NavigationItems = new ObservableCollection<NavigationItem>();
        }

        private void OnInizializeModule(InitializeModulePayload initializeModulePayload)
        {
            NavigationItems.Add(new NavigationItem(_eventAggregator, initializeModulePayload.ModuleIdentifier, initializeModulePayload.ResourceValue));
        }

        private readonly IEventAggregator _eventAggregator;
    }
}