using System.Collections.ObjectModel;
using CrossApplication.Core.Common;
using CrossApplication.Wpf.Common.Events;
using CrossApplication.Wpf.Common.Navigation;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;

namespace CrossApplication.Wpf.Application.Shell
{
    public class RichShellViewModel
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; set; }

        public DelegateCommand ManageAccountsCommand { get; }
        public InteractionRequest<INotification> NotificationRequest { get; }

        public RichShellViewModel(IEventAggregator eventAggregator, InteractionRequest<INotification> notificationRequest)
        {
            _eventAggregator = eventAggregator;
            NotificationRequest = notificationRequest;

            _eventAggregator.GetEvent<InitializeModuleEvent>().Subscribe(OnInizializeModule);

            NavigationItems = new ObservableCollection<NavigationItem>();
            ManageAccountsCommand = new DelegateCommand(() => NotificationRequest.Raise(new Notification {Title = LocalizationManager.Get(typeof(CrossApplication.Wpf.Common.Properties.Resources), "AccountManagementTitle")}));
        }

        private void OnInizializeModule(InitializeModulePayload initializeModulePayload)
        {
            NavigationItems.Add(new NavigationItem(_eventAggregator, initializeModulePayload.ModuleIdentifier, initializeModulePayload.ResourceValue));
        }

        private readonly IEventAggregator _eventAggregator;
    }
}