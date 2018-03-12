using System.Threading.Tasks;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Mail.Xamarin.Shell;

namespace CrossApplication.Mail.Xamarin
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
            _container.RegisterType<object, ShellView>("Mail", Lifetime.PerContainer);

            var shellViewItem = new ViewItem("Mail", RegionNames.MainRegion, true, RegionNames.RichRegion);
            _viewManager.AddViewItem(shellViewItem);
            return Task.CompletedTask;
        }

        public async Task ActivateAsync()
        {
            await _navigationService.NavigateToAsync("Mail");
        }

        private readonly IContainer _container;
        private readonly INavigationService _navigationService;
        private readonly IViewManager _viewManager;
    }
}