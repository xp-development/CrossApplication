using System;
using CrossApplication.Core.Common;
using CrossApplication.Core.Contracts;
using CrossApplication.Mail.Core.Navigation;
using CrossApplication.Mail.Wpf.Properties;
using CrossApplication.Mail.Wpf.Shell;
using CrossApplication.Wpf.Common;
using CrossApplication.Wpf.Common.Events;
using CrossApplication.Wpf.Contracts.Navigation;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;

namespace CrossApplication.Mail.Wpf
{
    [Module(ModuleName = "CrossApplication.Mail.Wpf")]
    public class MailModule : IModule
    {
        public Guid ModuleIdentifier => UniqueIdentifier.MailModuleIdentifier;

        public MailModule(IUnityContainer unityContainer, IEventAggregator eventAggregator, INavigationService navigationService, IViewManager viewManager)
        {
            _unityContainer = unityContainer;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _viewManager = viewManager;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<object, ShellView>(typeof(ShellView).FullName);
            _unityContainer.RegisterType<object, RibbonStartView>(typeof(RibbonStartView).FullName);

            _eventAggregator.GetEvent<ActivateModuleEvent>().Subscribe(OnActivateModule, true);
            _eventAggregator.GetEvent<InitializeModuleEvent>().Publish(new InitializeModulePayload(ModuleIdentifier, new ResourceValue(typeof(Resources), "ModuleName")));

            RegisterViews();
        }

        private void RegisterViews()
        {
            var shell = new ViewItem(ViewKeys.Shell, typeof(ShellView), true, RegionNames.MainRegion);
            shell.SubViewItems.Add(new ViewItem("Shell.Ribbon", typeof(RibbonStartView), false, RegionNames.RibbonRegion));

            _viewManager.AddViewItem(shell);
        }

        private void OnActivateModule(ActivateModulePayload activateModulePayload)
        {
            if(activateModulePayload.ModuleGuid != ModuleIdentifier)
                return;

            _navigationService.NavigateTo(ViewKeys.Shell);
        }

        private readonly IUnityContainer _unityContainer;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IViewManager _viewManager;
    }
}