using System.Threading.Tasks;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Modules.Settings.Core.Navigation;
using CrossApplication.Modules.Settings.Wpf.Shell;

namespace CrossApplication.Modules.Settings.Wpf
{
    [Module]
    public class Module : IModule
    {
        private readonly IViewManager _viewManager;
        private readonly IContainer _container;

        public Module(IViewManager viewManager, IContainer container)
        {
            _viewManager = viewManager;
            _container = container;
        }

        public Task InitializeAsync()
        {
            _container.RegisterType<ShellViewModel>();
            _container.RegisterType<object, ShellView>(ViewKeys.Shell, Lifetime.PerContainer);

            RegisterViews();

            return Task.CompletedTask;
        }


        public Task ActivateAsync()
        {
            return Task.CompletedTask;
        }

        private void RegisterViews()
        {
            _viewManager.RegisterRichShell(RegionNames.SettingsRegion, new ViewItem(ViewKeys.Shell, RegionNames.RichRegion));
        }
    }
}