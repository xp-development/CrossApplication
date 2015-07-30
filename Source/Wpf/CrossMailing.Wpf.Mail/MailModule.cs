using System;
using CrossMailing.Common;
using CrossMailing.Wpf.Common;
using CrossMailing.Wpf.Common.Events;
using CrossMailing.Wpf.Mail.Shell;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

namespace CrossMailing.Wpf.Mail
{
    public class MailModule : IModule
    {
        public Guid ModuleIdentifier => UniqueIdentifier.MailModuleIdentifier;

        public MailModule(IRegionManager regionManager, IUnityContainer unityContainer, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
            _eventAggregator = eventAggregator;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<object, ShellView>(typeof(ShellView).FullName);

            _eventAggregator.GetEvent<ActivateModuleEvent>().Subscribe(OnActivateModule, true);
            _eventAggregator.GetEvent<InitializeModuleEvent>().Publish(new InitializeModulePayload(ModuleIdentifier, new ResourceValue(typeof(Properties.Resources), "ModuleName")));
        }

        private void OnActivateModule(ActivateModulePayload activateModulePayload)
        {
            if(activateModulePayload.ModuleGuid != ModuleIdentifier)
                return;

            _regionManager.RequestNavigate(RegionNames.MainRegion, new Uri(typeof(ShellView).FullName, UriKind.Relative));
        }

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;
        private readonly IEventAggregator _eventAggregator;
    }
}