using System;
using CrossMailing.Wpf.Common;
using CrossMailing.Wpf.Common.Events;
using CrossMailing.Wpf.Mail.Shell;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace CrossMailing.Wpf.Mail
{
    public class MailModule : IModule
    {
        public MailModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<object, ShellView>(typeof(ShellView).FullName);

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ActivateMailModuleEvent>().Subscribe(OnActivateMailModule);
        }

        private void OnActivateMailModule(ActivateMailModulePayload activateMailModulePayload)
        {
            _regionManager.RequestNavigate(RegionNames.MainRegion, new Uri(typeof(ShellView).FullName, UriKind.Relative));
        }

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;
    }
}