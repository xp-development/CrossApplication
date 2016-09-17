using System;
using CrossApplication.Core.Common;
using CrossApplication.Mail.Wpf.Properties;
using CrossApplication.Mail.Wpf.Shell;
using CrossApplication.Wpf.Common;
using CrossApplication.Wpf.Common.Events;
using CrossApplication.Wpf.Contracts;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;

namespace CrossApplication.Mail.Wpf
{
    [Module(ModuleName = "CrossApplication.Mail.Wpf")]
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
            _eventAggregator.GetEvent<InitializeModuleEvent>().Publish(new InitializeModulePayload(ModuleIdentifier, new ResourceValue(typeof(Resources), "ModuleName")));

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