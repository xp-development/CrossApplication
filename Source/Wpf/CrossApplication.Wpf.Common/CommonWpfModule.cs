using CrossApplication.Core.Contracts;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Contracts.Navigation;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace CrossApplication.Wpf.Common
{
    [Module(ModuleName = "CrossApplication.Core.Wpf.Common")]
    public class CommonWpfModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public CommonWpfModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IViewManager, ViewManager>(new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<IUserManager, UserManager>(new ContainerControlledLifetimeManager());
        }
    }
}