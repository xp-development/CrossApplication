using System.Threading.Tasks;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Contracts.Navigation;

namespace CrossApplication.Wpf.Common
{
    [Module(Tag = ModuleTags.Infrastructure)]
    public class Module : IModule
    {
        private readonly IContainer _container;

        public Module(IContainer container)
        {
            _container = container;
        }

        public Task InitializeAsync()
        {
            _container.RegisterType<INavigationService, NavigationService>(Lifetime.PerContainer);
            _container.RegisterType<IViewManager, ViewManager>(Lifetime.PerContainer);
            _container.RegisterType<IUserManager, UserManager>(Lifetime.PerContainer);

            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            return Task.FromResult(false);
        }
    }
}