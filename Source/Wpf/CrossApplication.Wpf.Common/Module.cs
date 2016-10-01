using System.Threading.Tasks;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Navigation;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Contracts.Navigation;
using Microsoft.Practices.Unity;

namespace CrossApplication.Wpf.Common
{
    [Module(Tag = ModuleTags.Infrastructure)]
    public class Module : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public Module(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public Task InitializeAsync()
        {
            _unityContainer.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IViewManager, ViewManager>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IUserManager, UserManager>(new ContainerControlledLifetimeManager());

            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            return Task.FromResult(false);
        }
    }
}