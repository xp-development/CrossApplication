using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Contracts.Settings;
using CrossApplication.Core.Wpf.Common.Navigation;
using CrossApplication.Mail.Core.Navigation;
using CrossApplication.Mail.Wpf.Navigation;
using CrossApplication.Mail.Wpf.Settings;
using CrossApplication.Mail.Wpf.Shell;

namespace CrossApplication.Mail.Wpf
{
    [Module]
    public class Module : IModule
    {
        public Module(IContainer container, INavigationService navigationService, IViewManager viewManager)
        {
            _container = container;
            _navigationService = navigationService;
            _viewManager = viewManager;
        }

        public Task InitializeAsync()
        {
            _container.RegisterType<ShellViewModel>();
            _container.RegisterType<object, ShellView>(ViewKeys.Shell, Lifetime.PerContainer);

            _container.RegisterType<ISettingsChild, SettingsViewModel, SettingsViewModel>(Lifetime.PerContainer);
            _container.RegisterType<object, SettingsView>(ViewKeys.Settings, Lifetime.PerContainer);

            _container.RegisterType<NavigationViewModel>();
            _container.RegisterType<object, NavigationView>(typeof(NavigationView).FullName, Lifetime.PerContainer);

            RegisterViews();
            return Task.CompletedTask;
        }

        public async Task ActivateAsync()
        {
            await _navigationService.NavigateToAsync(ViewKeys.Shell);
        }

        private void RegisterViews()
        {
            var shellViewItem = new ViewItem(ViewKeys.Shell, RegionNames.MainRegion, true, RegionNames.RichRegion);
            shellViewItem.SubViewItems.Add(new ViewItem(typeof(NavigationView).FullName, MailRegionNames.NavigationRegion));
            _viewManager.AddViewItem(shellViewItem);

            _viewManager.AddViewItem(new ViewItem(ViewKeys.Settings, RegionNames.SettingsRegion, true, RegionNames.SettingsRegion));
        }

        private readonly IContainer _container;
        private readonly INavigationService _navigationService;
        private readonly IViewManager _viewManager;
    }
}