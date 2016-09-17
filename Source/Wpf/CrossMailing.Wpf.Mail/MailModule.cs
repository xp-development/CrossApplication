using System;
using CrossMailing.Common;
using CrossMailing.Wpf.Contracts;
using CrossMailing.Wpf.Common;
using CrossMailing.Wpf.Common.Events;
using CrossMailing.Wpf.Mail.Shell;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;

namespace CrossMailing.Wpf.Mail
{
    [Module(ModuleName = "CrossMailing.Wpf.Mail")]
    public class MailModule : IModule
    {
        public Guid ModuleIdentifier => UniqueIdentifier.MailModuleIdentifier;

        public MailModule(IUnityContainer unityContainer, IEventAggregator eventAggregator, INavigationService navigationService)
        {
            _unityContainer = unityContainer;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<object, ShellView>(typeof(ShellView).FullName);
            _unityContainer.RegisterType<object, RibbonStartView>(typeof(RibbonStartView).FullName);

            _eventAggregator.GetEvent<ActivateModuleEvent>().Subscribe(OnActivateModule, true);
            _eventAggregator.GetEvent<InitializeModuleEvent>().Publish(new InitializeModulePayload(ModuleIdentifier, new ResourceValue(typeof(Properties.Resources), "ModuleName")));

            _navigationService.RegisterView<ShellView>("ShellView", RegionNames.MainRegion);
            _navigationService.RegisterView<RibbonStartView>("RibbonStartView", RegionNames.RibbonRegion);
        }

        private void OnActivateModule(ActivateModulePayload activateModulePayload)
        {
            if(activateModulePayload.ModuleGuid != ModuleIdentifier)
                return;

            _navigationService.NavigateTo("ShellView");
            _navigationService.NavigateTo("RibbonStartView");
        }

        private readonly IUnityContainer _unityContainer;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
    }
}