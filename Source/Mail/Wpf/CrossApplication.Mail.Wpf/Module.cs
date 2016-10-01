using System.Threading.Tasks;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Navigation;
using CrossApplication.Mail.Core.Navigation;
using CrossApplication.Mail.Wpf.Shell;
using CrossApplication.Wpf.Common;
using CrossApplication.Wpf.Contracts.Navigation;
using Microsoft.Practices.Unity;

namespace CrossApplication.Mail.Wpf
{
    [Module]
    public class Module : IModule
    {
        public Module(IUnityContainer unityContainer, INavigationService navigationService, IViewManager viewManager)
        {
            _unityContainer = unityContainer;
            _navigationService = navigationService;
            _viewManager = viewManager;
        }

        private void RegisterViews()
        {
            var shell = new ViewItem(ViewKeys.Shell, typeof(ShellView), false, RegionNames.MainRegion);
            shell.SubViewItems.Add(new ViewItem("Shell.Ribbon", typeof(RibbonStartView), false, RegionNames.RibbonRegion));

            _viewManager.AddViewItem(shell);
        }

        public Task InitializeAsync()
        {
            _unityContainer.RegisterType<object, ShellView>(typeof(ShellView).FullName);
            _unityContainer.RegisterType<object, RibbonStartView>(typeof(RibbonStartView).FullName);

            RegisterViews();
            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            _navigationService.NavigateTo(ViewKeys.Shell);
            return Task.FromResult(false);
        }

        private readonly INavigationService _navigationService;
        private readonly IUnityContainer _unityContainer;
        private readonly IViewManager _viewManager;
    }
}