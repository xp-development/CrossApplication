using System.Collections.ObjectModel;
using CrossMailing.Wpf.Common.Events;
using Microsoft.Practices.Prism.PubSubEvents;

namespace CrossMailing.Wpf.Application.Shell
{
    public class RichShellViewModel
    {
        public ObservableCollection<string> NavigationItems { get; set; }

        public RichShellViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<InitializeModuleEvent>().Subscribe(OnInizializeModule);

            NavigationItems = new ObservableCollection<string>();
        }

        private void OnInizializeModule(InitializeModulePayload initializeModulePayload)
        {
            NavigationItems.Add(initializeModulePayload.ModuleIdentifier.ToString());
        }
    }
}