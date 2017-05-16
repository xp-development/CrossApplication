using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Xamarins.Shell;
using CrossApplication.Mail.Core.Navigation;

namespace CrossApplication.Mail.Xamarins
{
    [Module]
    public class Module : IModule
    {
        public Module(IViewManager viewManager, INavigationService navigationService, IContainer container)
        {
            _viewManager = viewManager;
            _navigationService = navigationService;
            _container = container;
        }

        public Task InitializeAsync()
        {
            _container.RegisterType<RichShellViewModel>();
            _container.RegisterType<object, RichShellView>(ViewKeys.Shell, Lifetime.PerContainer);

            var shellViewItem = new ViewItem(ViewKeys.Shell, "");
            _viewManager.AddViewItem(shellViewItem);
            return Task.FromResult(false);
        }

        public async Task ActivateAsync()
        {
            await _navigationService.NavigateToAsync(ViewKeys.Shell);
        }

        private readonly IViewManager _viewManager;
        private readonly INavigationService _navigationService;
        private readonly IContainer _container;
    }
}